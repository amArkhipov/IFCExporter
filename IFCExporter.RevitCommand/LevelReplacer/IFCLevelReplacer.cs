using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCExporter.RevitCommand
{
    public class IFCLevelReplacer : ILevelReplacer
    {
        private readonly string pathToIFC;
        public IFCLevelReplacer(string PathToIFC)
        {
            pathToIFC = PathToIFC;
        }

        public void Replece(Dictionary<string, string> replacements)
        {
            // Читаем весь текст из файла
            string content = File.ReadAllText(pathToIFC);


            foreach (var replacement in replacements)
            {
                content = content.Replace(replacement.Key, replacement.Value);
            }

            // Записываем изменённый контент обратно в файл
            File.WriteAllText(pathToIFC, content);

        }
    }
}
