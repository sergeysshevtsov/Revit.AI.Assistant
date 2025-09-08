using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.External;
using Revit.AI.Assistant.Services;

namespace Revit.AI.Assistant.Commands;

[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class CmdAIAssistant : ExternalCommand
{
    public override void Execute()
    {
        try
        {
            DockablePaneService.SetWindowVisibility(ExternalCommandData.Application, Assistant.Application.dockablePaneId);
        }
        catch (Exception ex)
        {
            TaskDialog.Show("Error", "Error showing AI Assistant panel: " + ex.Message);
        }
    }
}