using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCExporter.RevitCommand
{
    internal interface IExportInfo
    {
        string Name { get; }
        string FilePath { get; }
        IFCExportOptions ExportOptions { get; }
    }
}
