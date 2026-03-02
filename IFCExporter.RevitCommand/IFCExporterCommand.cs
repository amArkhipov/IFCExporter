using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

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
            Dictionary<string, string> replacements;
            try
            {
                replacements = LevelMapping(document);
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
            var encoder = new KirillicEncoder();

            var levels = levelReader
                .ReadAll()
                .Select(v => new KeyValuePair<string, string>(v.Name, levelMapper.Map(v.Name)))
                .ToList();

            return levels.ToDictionary
                (kv => encoder.EncodeToEscapeSequence(kv.Key),
                kv => encoder.EncodeToEscapeSequence(kv.Value));
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
