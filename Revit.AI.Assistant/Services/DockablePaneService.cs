using Autodesk.Revit.UI;

namespace Revit.AI.Assistant.Services;
internal class DockablePaneService
{
    public static IReadOnlyDictionary<Guid, IDockablePaneProvider> DockablePaneProviders { get; private set; }

    public static void RegisterDockableWindow(UIControlledApplication application, IDockablePaneProvider window, DockablePaneId dockablePaneId, string dockablePaneTitle)
    {
        try
        {
            application.RegisterDockablePane(dockablePaneId, dockablePaneTitle, window);
            AddDockableProvider(window, dockablePaneId);
        }
        catch (Exception ex)
        {
            TaskDialog.Show(dockablePaneTitle, ex.Message);
        }
    }

    public static void SetWindowVisibility(UIApplication application, DockablePaneId paneId)
    {
        try
        {
            if (application.GetDockablePane(paneId) is DockablePane pane)
            {
                if (DockablePane.PaneIsRegistered(pane.Id) && DockablePane.PaneExists(pane.Id))
                {
                    if (!pane.IsShown())
                        pane.Show();
                    else
                        pane.Hide();
                }
            }
        }
        catch (Exception ex)
        {
            TaskDialog.Show("Exception", ex.Message);
        }
    }

    public static DockablePane GetPaneWindow(UIControlledApplication application, DockablePaneId paneId)
    {
        try
        {
            var pane = application.GetDockablePane(paneId);
            return pane;
        }
        catch (Exception ex)
        {
            TaskDialog.Show("Exception", ex.Message);
            return null;
        }
    }

    public static DockablePane GetPaneWindow(UIApplication application, DockablePaneId paneId)
    {
        try
        {
            var pane = application.GetDockablePane(paneId);
            return pane;
        }
        catch (Exception ex)
        {
            TaskDialog.Show("Exception", ex.Message);
            return null;
        }
    }

    private static void AddDockableProvider(IDockablePaneProvider window, DockablePaneId paneId)
    {
        if (DockablePaneProviders == null)
            DockablePaneProviders = new Dictionary<Guid, IDockablePaneProvider> { { paneId.Guid, window } };
        else
        {
            var newDictionary = DockablePaneProviders.ToDictionary(d => d.Key, d => d.Value);
            newDictionary.Add(paneId.Guid, window);
            DockablePaneProviders = new Dictionary<Guid, IDockablePaneProvider>(newDictionary);
        }
    }
}
