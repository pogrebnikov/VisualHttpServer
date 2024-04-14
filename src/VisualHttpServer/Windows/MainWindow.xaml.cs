using System.Windows;
using System.Windows.Controls;
using VisualHttpServer.Abstractions;
using VisualHttpServer.Commands;

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
    }

    public int SelectedRoutesCount => RoutesListView.SelectedItems.Count;
    public event EventHandler? RoutesSelectionChanged;

    private void RoutesListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        RoutesSelectionChanged?.Invoke(this, EventArgs.Empty);
    }
}