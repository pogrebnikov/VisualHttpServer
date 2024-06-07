using System.Collections;
using System.Windows.Input;
using VisualHttpServer.Abstractions;
using VisualHttpServer.Model;

namespace VisualHttpServer.Commands;

internal class EnableRoutesCommand(RouteUiCollection routeCollection) : ICommand
{
    private IRoutesView? _routesView;

    public bool CanExecute(object? parameter)
    {
        return GetCanExecute();
    }

    public void Execute(object? parameter)
    {
        if (parameter is not IList list)
        {
            return;
        }

        var routes = list.OfType<RouteUi>().ToArray();
        foreach (var route in routes)
        {
            if (route.Disabled)
            {
                route.Enable();
            }
        }

        routeCollection.Update();
        OnCanExecuteChanged();
        Executed?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? CanExecuteChanged;

    public event EventHandler? Executed;

    public void SetRoutesView(IRoutesView routesView)
    {
        if (_routesView is not null)
        {
            throw new InvalidOperationException($"{nameof(routesView)} has already set.");
        }

        _routesView = routesView;
        _routesView.RoutesSelectionChanged += RoutesView_RoutesSelectionChanged;
    }

    private void RoutesView_RoutesSelectionChanged(object? sender, EventArgs e)
    {
        OnCanExecuteChanged();
    }

    private void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    private bool GetCanExecute()
    {
        if (_routesView is null)
        {
            return false;
        }

        if (_routesView.SelectedRoutesCount == 0)
        {
            return false;
        }

        var anyDisabled = _routesView.SelectedRoutes.Any(route => route.Disabled);

        return anyDisabled;
    }
}