using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Text;

namespace IFCExporter.RevitCommand
{
    [Transaction(TransactionMode.Manual)]
    public class IFCExporterCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;


            var exportInfo = new DefaultExportInfo();
            var logger = new Logger();
            try
            {
                Dictionary<string, string> replacements = LevelMapping(document);
                ExportIFC(document, exportInfo);
                LevelReplacing(exportInfo, replacements);

                logger.LogInfo($"Выгрузка {exportInfo.Name}.ifc" +
                    $"\nна рабочий стол завершена");

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                message += ex.StackTrace;
                return Result.Failed;
            }
        }

        private Dictionary<string, string> LevelMapping(Document document)
        {

            var levelReader = new LevelReader(document);
            var levelMapper = new MGEMapper();

            var levels = levelReader
                .ReadAll()
                .Select(v => new KeyValuePair<string, string>(v.Name, levelMapper.Map(v.Name)))
                .ToList();

            return levels.ToDictionary(kv => EncodeToEscapeSequence(kv.Key), kv => EncodeToEscapeSequence(kv.Value));
        }

        private void LevelReplacing(DefaultExportInfo exportInfo, Dictionary<string, string> replacements)
        {
            var pathToIFC = Path.Combine(exportInfo.FilePath, exportInfo.Name + ".ifc");

            string content = File.ReadAllText(pathToIFC);

            foreach (var replacement in replacements)
            {
                content = content.Replace(replacement.Key, replacement.Value);
            }

            // Записываем изменённый контент обратно в файл
            File.WriteAllText(pathToIFC, content);

        }
        public static string EncodeToEscapeSequence(string text)
        {
            StringBuilder builder = new StringBuilder();
            for (var i = 0; i < text.Length;)
            {
                if (IsKirillicSymbol(text[i])) // Проверяем, является ли символ кириллическим
                {
                    builder.Append(@"\X2\");
                    while (i < text.Length && IsKirillicSymbol(text[i]))
                    {
                        ushort codePoint = (ushort)text[i];
                        // Форматируем число в четырехзначный HEX
                        string hexCode = codePoint.ToString("X4");
                        builder.Append(hexCode);
                        i++;
                    }
                    builder.Append(@"\X0\");
                }
                else
                {
                    builder.Append(text[i]); // Оставляем остальные символы неизменёнными
                    i++;
                }
            }
            return builder.ToString();
        }

        private static bool IsKirillicSymbol(char ch)
        {
            // Диапазоны для кириллического диапазона Unicode
            return (ch >= '\u0400' && ch <= '\u04FF');
        }

        private void ExportIFC(Document document, DefaultExportInfo exportInfo)
        {
            using (var tr = new Transaction(document, "ExportIFC"))
            {
                tr.Start();
                document.Export(exportInfo.FilePath, exportInfo.Name, exportInfo.ExportOptions);
                tr.Commit();
            }
        }
    }
}
