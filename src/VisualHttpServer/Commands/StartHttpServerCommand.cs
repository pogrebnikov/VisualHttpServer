using System.Net;
using System.Windows.Input;
using System.Windows.Threading;
using VisualHttpServer.Core;
using VisualHttpServer.Services;

namespace VisualHttpServer.Commands;

internal class StartHttpServerCommand : ICommand
{
    private readonly IHttpServer _httpServer;
    private readonly IMessageViewer _messageViewer;
    private bool _canExecute;

    public StartHttpServerCommand(IHttpServer httpServer, IMessageViewer messageViewer)
    {
        _httpServer = httpServer;
        _messageViewer = messageViewer;

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
        if (parameter == null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        var connectionSettings = (ConnectionSettings)parameter;

        if (!TryParseIpAddress(connectionSettings, out var address) || address == null)
        {
            return;
        }

        if (!TryParsePort(connectionSettings, out var port))
        {
            return;
        }

        try
        {
            _httpServer.Start(address, port);
        }
        catch
        {
            _messageViewer.View("Error!", "Can't start a server. Please check a host and a port.");
        }
    }

    public event EventHandler? CanExecuteChanged;

    private bool GetCanExecute()
    {
        return _httpServer.State == HttpServerState.Stopped;
    }

    private bool TryParseIpAddress(ConnectionSettings connectionSettings, out IPAddress? address)
    {
        address = null;

        var host = connectionSettings.Host;
        if (string.IsNullOrWhiteSpace(host))
        {
            _messageViewer.View("Warning!", "Please enter a host.");
            return false;
        }

        try
        {
            address = IPAddress.Parse(host);
            return true;
        }
        catch
        {
            _messageViewer.View("Warning!", $"The host '{host}' is invalid.");
            return false;
        }
    }

    private bool TryParsePort(ConnectionSettings connectionSettings, out int port)
    {
        port = 0;

        var portAsStr = connectionSettings.Port;
        if (string.IsNullOrWhiteSpace(portAsStr))
        {
            _messageViewer.View("Warning!", "Please enter a port.");
            return false;
        }

        try
        {
            port = int.Parse(portAsStr);
            return true;
        }
        catch
        {
            _messageViewer.View("Warning!", $"The port '{portAsStr}' is invalid.");
            return false;
        }
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