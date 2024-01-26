using System.Windows.Input;
using VisualHttpServer.Core;

namespace VisualHttpServer.Commands;

internal class StopHttpServerCommand(IHttpServer httpServer) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        httpServer.Stop();
    }

    public event EventHandler? CanExecuteChanged;
}