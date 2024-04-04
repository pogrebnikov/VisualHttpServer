using System.Windows.Input;
using System.Windows;
using VisualHttpServer.Model;

namespace VisualHttpServer.Commands;

internal class ClearRoutesCommand(RouteUiCollection routes) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        const string message = "Are you sure you want to clear the route list?";
        if (MessageBox.Show(message, "Clear routes", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            routes.Clear();
        }
    }

    public event EventHandler? CanExecuteChanged;
}
