using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.External;
using Revit.AI.Assistant.Commands;
using Revit.AI.Assistant.Services;
using Revit.AI.Assistant.UI;
using Revit.Async;

namespace Revit.AI.Assistant;

[UsedImplicitly]
public class Application : ExternalApplication
{

    public static DockablePaneId dockablePaneId = new(Guid.Parse("840DEBE3-C9AD-4451-B972-B179E8C72CE6"));
    readonly string dockablePaneTitle = "AI.Assistant - Local - Ollama";

    public override void OnStartup()
    {
        CreateRibbon();
        CreateDockablePanes(Application);
        RevitTask.Initialize(Application);
    }

    private void CreateRibbon()
    {
        var panel = Application.CreatePanel("Utils", "SHSS Tools");
        panel.AddPushButton<CmdAIAssistant>("AI.Assistant")
            .SetImage("/Revit.AI.Assistant;component/Resources/Icons/AIAssistant16.png")
            .SetLargeImage("/Revit.AI.Assistant;component/Resources/Icons/AIAssistant32.png");
    }

    private bool CreateDockablePanes(UIControlledApplication application)
    {
        try
        {
            var uc = new AIPanelUserControl();
            DockablePaneService.RegisterDockableWindow(application, uc, dockablePaneId, dockablePaneTitle);
        }
        catch
        {
            return false;
        }
        return true;
    }
}