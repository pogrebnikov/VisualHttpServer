using System.Collections.ObjectModel;
using System.Windows.Threading;
using VisualHttpServer.Commands;
using VisualHttpServer.Core;

namespace VisualHttpServer;

internal class MainWindowViewModel
{
    private readonly IHttpServer? _httpServer;

    public MainWindowViewModel()
    {
        _httpServer = ServiceLocator.Resolve<IHttpServer>();
        var dispatcherTimer = new DispatcherTimer
        {
            Interval = new TimeSpan(0, 0, 0, 0, 100)
        };

        dispatcherTimer.Tick += DispatcherTimer_Tick;
        dispatcherTimer.Start();
    }

    public ConnectionSettings ConnectionSettings { get; } = new()
    {
        Host = "127.0.0.1",
        Port = "8080"
    };

    public ObservableCollection<Interaction> UnhandledRequests { get; } = new();

    public StartHttpServerCommand? StartHttpServer { get; } = ServiceLocator.Resolve<StartHttpServerCommand>();

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        if (_httpServer != null)
        {
            foreach (var interaction in _httpServer.UnhandledInteractions.PopAll())
            {
                UnhandledRequests.Insert(0, interaction);
            }
        }
    }
}