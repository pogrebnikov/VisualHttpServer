using System.Windows;
using System.Windows.Controls;
using VisualHttpServer.Abstractions;
using VisualHttpServer.Commands;
using VisualHttpServer.Model;

namespace VisualHttpServer.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IRoutesView
{
    public MainWindow()
    {
        InitializeComponent();

        var removeRouteCommand = ServiceLocator.Resolve<RemoveRoutesCommand>();
        removeRouteCommand?.SetRoutesView(this);

        var disableRoutesCommand = ServiceLocator.Resolve<DisableRoutesCommand>();
        disableRoutesCommand?.SetRoutesView(this);

        var enableRoutesCommand = ServiceLocator.Resolve<EnableRoutesCommand>();
        enableRoutesCommand?.SetRoutesView(this);
    }

    public int SelectedRoutesCount => RoutesListView.SelectedItems.Count;
    
    public IReadOnlyCollection<RouteUi> SelectedRoutes
    {
        get
        {
            var routes = RoutesListView.SelectedItems.OfType<RouteUi>().ToArray();
            return routes;
        }
    }

    public event EventHandler? RoutesSelectionChanged;

    private void RoutesListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        RoutesSelectionChanged?.Invoke(this, EventArgs.Empty);
    }
}