using System.Collections;
using System.Windows.Input;
using VisualHttpServer.Abstractions;
using VisualHttpServer.Model;

namespace VisualHttpServer.Commands;

internal class RemoveRoutesCommand : ICommand
{
    private readonly RouteUiCollection _routes;
    private bool _canExecute;
    private IRoutesView? _routesView;

    public RemoveRoutesCommand(RouteUiCollection routes)
    {
        _routes = routes;
        _canExecute = GetCanExecute();
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute;
    }

    public void Execute(object? parameter)
    {
        if (parameter is IList list)
        {
            var routes = list.OfType<RouteUi>().ToArray();
            _routes.RemoveRange(routes);
        }
    }

    public event EventHandler? CanExecuteChanged;

    public void SetRoutesView(IRoutesView routesView)
    {
        _routesView = routesView;
        _routesView.RoutesSelectionChanged += RoutesView_RoutesSelectionChanged;
    }

    private void RoutesView_RoutesSelectionChanged(object? sender, EventArgs e)
    {
        var canExecute = GetCanExecute();
        if (_canExecute == canExecute)
        {
            return;
        }

        _canExecute = canExecute;
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    private bool GetCanExecute()
    {
        if (_routesView is null)
        {
            return false;
        }

        return _routesView.SelectedRoutesCount > 0;
    }
}