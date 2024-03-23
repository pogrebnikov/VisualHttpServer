using System.Windows.Input;
using VisualHttpServer.Windows;

namespace VisualHttpServer.Commands;

internal class NewRouteCommand : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        NewRouteWindow window = new();
        window.Show();
    }

    public event EventHandler? CanExecuteChanged;
}