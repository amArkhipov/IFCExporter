using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCExporter.RevitCommand
{
    internal readonly struct DefaultExportInfo : IExportInfo
    {
        public string Name => Path.GetRandomFileName();

        public string FilePath => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public IFCExportOptions ExportOptions => new IFCExportOptions();
    }
}
