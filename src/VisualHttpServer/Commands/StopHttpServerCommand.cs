using System.Windows.Input;
using System.Windows.Threading;
using VisualHttpServer.Core;

namespace VisualHttpServer.Commands;

internal class StopHttpServerCommand : ICommand
{
    private readonly IHttpServer _httpServer;
    private bool _canExecute;

    public StopHttpServerCommand(IHttpServer httpServer)
    {
        _httpServer = httpServer;

        var dispatcherTimer = new DispatcherTimer
        {
            Interval = new TimeSpan(0, 0, 0, 0, 100)
        };

        dispatcherTimer.Tick += DispatcherTimer_Tick;
        dispatcherTimer.Start();
        _canExecute = GetCanExecute();
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute;
    }

    public void Execute(object? parameter)
    {
        _httpServer.Stop();
    }

    public event EventHandler? CanExecuteChanged;

    private bool GetCanExecute()
    {
        return _httpServer.State == HttpServerState.Started;
    }

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        var newCanExecute = GetCanExecute();

        if (_canExecute != newCanExecute)
        {
            _canExecute = newCanExecute;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}