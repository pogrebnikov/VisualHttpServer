using VisualHttpServer.Commands;

namespace VisualHttpServer;

internal class MainWindowViewModel
{
    public ConnectionSettings ConnectionSettings { get; } = new()
    {
        Host = "127.0.0.1",
        Port = "8080"
    };

    public StartHttpServerCommand? StartHttpServer { get; } = ServiceLocator.Resolve<StartHttpServerCommand>();
}