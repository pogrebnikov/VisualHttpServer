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
            if (!Validate(route))
            {
                return;
            }
            // Clone route because NewRouteWindowViewModel subscribers on PropertyChanged events of source route
            routes.Add(route.Clone());
        }

        CloseWindowAction?.Invoke();
    }

    public event EventHandler? CanExecuteChanged;

    private bool Validate(RouteUi route)
    {
        if (string.IsNullOrWhiteSpace(route.Method) || string.IsNullOrWhiteSpace(route.Path))
        {
            messageViewer.View(string.Empty, "Please fill required (*) fields.");
            return false;
        }

        if (routes.Contains(route))
        {
            messageViewer.View(string.Empty, "A route with same Method and Path already exists.");
            return false;
        }

        return true;
    }
}