using Autodesk.Revit.UI;
using System.Windows.Controls;

namespace Revit.AI.Assistant.UI;
public partial class AIPanelUserControl : UserControl, IDockablePaneProvider
{
    public AIPanelUserControl()
    {
        InitializeComponent();
        DataContext = new AIPanelUserControlDataContext();
    }

    public void SetupDockablePane(DockablePaneProviderData data)
    {
        data.FrameworkElement = this;
        data.InitialState = new DockablePaneState { DockPosition = DockPosition.Right };
        data.VisibleByDefault = false;
    }
}
