using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCExporter.RevitCommand
{
    public class Logger : iLogger
    {
        public void LogInfo(string message)
        {
            TaskDialog mainDialog = new TaskDialog("Info");
            mainDialog.MainIcon = TaskDialogIcon.TaskDialogIconInformation;
            mainDialog.MainInstruction = message;
            mainDialog.Show();
        }
        public void LogError(Exception ex)
        {
            TaskDialog mainDialog = new TaskDialog(ex.Source);
            mainDialog.MainIcon = TaskDialogIcon.TaskDialogIconError;
            mainDialog.MainInstruction = ex.Source;
            mainDialog.MainContent = ex.Message;
            mainDialog.ExpandedContent = ex.StackTrace;
            mainDialog.Show();
        }
    }
}
