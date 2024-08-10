using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        var editRouteCommand = ServiceLocator.Resolve<EditRouteCommand>();
        editRouteCommand?.SetRoutesView(this);

        var duplicateRouteCommand = ServiceLocator.Resolve<DuplicateRouteCommand>();
        duplicateRouteCommand?.SetRoutesView(this);

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

    public void RefreshRoutesListView()
    {
        RoutesListView.Items.Refresh();
    }

    private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

    private void RoutesListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        RoutesSelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void RouteListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is not ListViewItem { Content: RouteUi route })
        {
            return;
        }

        ViewModel.EditRoute?.Execute(route);
    }

    private void RouteListView_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete)
        {
            ViewModel.RemoveRoutes!.Execute(SelectedRoutes);
        }
    }
}