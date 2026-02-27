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

            try
            {
                ExportIFC(document);
                return Result.Succeeded;
            }
            catch (Exception ex) 
            {
                message = ex.Message;
                message += ex.StackTrace;
                return Result.Failed;
            }
        }

        private void ExportIFC(Document document)
        {
            var exportInfo = new DefaultExportInfo();
            using (var tr = new Transaction(document, "ExportIFC"))
            {
                tr.Start();
                document.Export(exportInfo.FilePath, exportInfo.Name, exportInfo.ExportOptions);
                tr.Commit();
            }
        }
    }
}
