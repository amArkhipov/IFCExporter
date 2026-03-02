using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCExporter.RevitCommand
{
    public readonly struct DefaultExportInfo : IExportInfo
    {
        public DefaultExportInfo()
        {
            Name = Path.GetRandomFileName();
            FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ExportOptions = new IFCExportOptions();
        }

        public readonly string Name { get; }
        public readonly string FilePath { get; }
        public readonly IFCExportOptions ExportOptions { get; }
    }
}
