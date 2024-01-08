using System.Net;
using System.Windows.Input;
using VisualHttpServer.Core;
using VisualHttpServer.Services;

namespace VisualHttpServer.Commands;

internal class StartHttpServerCommand(IHttpServer httpServer, IMessageViewer messageViewer) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
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
            httpServer.Start(address, port);
        }
        catch
        {
            messageViewer.View("Error!", "Can't start a server. Please check a host and a port.");
        }
    }

    public event EventHandler? CanExecuteChanged;

    private bool TryParseIpAddress(ConnectionSettings connectionSettings, out IPAddress? address)
    {
        address = null;

        var host = connectionSettings.Host;
        if (string.IsNullOrWhiteSpace(host))
        {
            messageViewer.View("Warning!", "Please enter a host.");
            return false;
        }

        try
        {
            address = IPAddress.Parse(host);
            return true;
        }
        catch
        {
            messageViewer.View("Warning!", $"The host '{host}' is invalid.");
            return false;
        }
    }

    private bool TryParsePort(ConnectionSettings connectionSettings, out int port)
    {
        port = 0;

        var portAsStr = connectionSettings.Port;
        if (string.IsNullOrWhiteSpace(portAsStr))
        {
            messageViewer.View("Warning!", "Please enter a port.");
            return false;
        }

        try
        {
            port = int.Parse(portAsStr);
            return true;
        }
        catch
        {
            messageViewer.View("Warning!", $"The port '{portAsStr}' is invalid.");
            return false;
        }
    }
}