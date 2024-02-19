using System.Windows.Input;
using VisualHttpServer.Model;
using VisualHttpServer.Services;

namespace VisualHttpServer.Commands;

internal class CreateRouteCommand(RouteUiCollection routes, IMessageViewer messageViewer) : ICommand
{
    public Action? CloseWindowAction { get; set; }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is RouteUi route)
        {
            if (!Validate(route, routes))
            {
                return;
            }
            routes.Add(route);
        }

        CloseWindowAction?.Invoke();
    }

    public event EventHandler? CanExecuteChanged;

    private bool Validate(RouteUi route, RouteUiCollection routes)
    {
        if (string.IsNullOrWhiteSpace(route.Method) || string.IsNullOrWhiteSpace(route.Path))
        {
            messageViewer.View(string.Empty, "Please fill required (*) fields.");
            return false;
        }

        if (routes.Any(rt => rt.Method == route.Method && rt.Path == route.Path))
        {
            messageViewer.View(string.Empty, "A route with same Method and Path already exists.");
            return false;
        }

        return true;
    }
}