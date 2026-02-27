using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace IFCExporter.RevitCommand
{
    public class IFCExporterCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex) 
            {
                message = ex.Message;
                message += ex.StackTrace;
                return Result.Failed;
            }
        }
    }
}
