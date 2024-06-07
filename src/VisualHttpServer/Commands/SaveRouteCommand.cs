using System.Windows.Input;
using VisualHttpServer.Abstractions;
using VisualHttpServer.Model;

namespace VisualHttpServer.Commands;

internal class SaveRouteCommand(RouteUiCollection routes) : ICommand
{
    private Action? _closeEditWindowAction;
    private RouteUi? _editedRoute;
    private IRoutesView? _routesView;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (_routesView is null)
        {
            throw new InvalidOperationException($"{nameof(_routesView)} is null.");
        }

        if (_closeEditWindowAction is null)
        {
            throw new InvalidOperationException($"{nameof(_closeEditWindowAction)} is null.");
        }

        if (parameter is not RouteUi changedRoute || _editedRoute is null)
        {
            return;
        }

        _editedRoute.Update(changedRoute);
        _routesView.RefreshRoutesListView();

        routes.Update();

        _closeEditWindowAction();
    }

    public event EventHandler? CanExecuteChanged;

    public void SetEditedRoute(RouteUi route)
    {
        _editedRoute = route;
    }

    public void SetRoutesView(IRoutesView routesView)
    {
        _routesView = routesView;
    }

    public void SetCloseEditWindowAction(Action closeEditWindowAction)
    {
        _closeEditWindowAction = closeEditWindowAction;
    }
}