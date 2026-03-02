using Autodesk.Revit.DB;

namespace IFCExporter.RevitCommand
{
    public readonly struct ExportInfo : IExportInfo
    {
        public ExportInfo(string nameIFC, string filePath, IFCExportOptions exportOptions)
        {
            Name = nameIFC;
            FilePath = filePath;
            ExportOptions = exportOptions;
        }

        public readonly string Name { get; }
        public readonly string FilePath { get; }
        public readonly IFCExportOptions ExportOptions { get; }
    }


}
