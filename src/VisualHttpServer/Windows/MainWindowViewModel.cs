using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using VisualHttpServer.Commands;
using VisualHttpServer.Core;
using VisualHttpServer.Model;

namespace VisualHttpServer.Windows;

internal class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly IHttpServer? _httpServer;

    public MainWindowViewModel()
    {
        _httpServer = ServiceLocator.Resolve<IHttpServer>();

        var routes = ServiceLocator.Resolve<RouteUiCollection>();
        Routes = routes is null ? new ObservableCollection<RouteUi>() : routes.AsObservable();

        if (DisableRoutes is not null)
        {
            DisableRoutes.CanExecuteChanged += DisableRoutes_CanExecuteChanged;
            DisableRoutes.Executed += DisableRoutes_Executed;
        }

        if (EnableRoutes is not null)
        {
            EnableRoutes.CanExecuteChanged+= EnableRoutes_CanExecuteChanged;
            EnableRoutes.Executed += EnableRoutes_Executed;
        }

        var dispatcherTimer = new DispatcherTimer
        {
            Interval = new TimeSpan(0, 0, 0, 0, 100)
        };

        dispatcherTimer.Tick += DispatcherTimer_Tick;
        dispatcherTimer.Start();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public ConnectionSettings ConnectionSettings { get; } = new()
    {
        Host = "127.0.0.1",
        Port = "8080"
    };

    public ObservableCollection<RouteUi> Routes { get; }
    public ObservableCollection<Interaction> HandledRequests { get; } = new();
    public ObservableCollection<Interaction> UnhandledRequests { get; } = new();

    public NewRouteCommand NewRoute { get; } = new();
    
    public DisableRoutesCommand? DisableRoutes { get; } = ServiceLocator.Resolve<DisableRoutesCommand>();
    public Visibility DisableRoutesVisibility { get; set; } = Visibility.Collapsed;
    
    public EnableRoutesCommand? EnableRoutes { get; } = ServiceLocator.Resolve<EnableRoutesCommand>();
    public Visibility EnableRoutesVisibility { get; set; } = Visibility.Collapsed;
    
    public RemoveRoutesCommand? RemoveRoutes { get; } = ServiceLocator.Resolve<RemoveRoutesCommand>();
    
    public StartHttpServerCommand? StartHttpServer { get; } = ServiceLocator.Resolve<StartHttpServerCommand>();
    public Visibility StartHttpServerVisibility { get; set; } = Visibility.Visible;
    
    public StopHttpServerCommand? StopHttpServer { get; } = ServiceLocator.Resolve<StopHttpServerCommand>();
    public Visibility StopHttpServerVisibility { get; set; } = Visibility.Collapsed;

    public ClearRoutesCommand? ClearRoutes { get; } = ServiceLocator.Resolve<ClearRoutesCommand>();
    public AboutProgramCommand AboutProgram { get; } = new();

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void DisableRoutes_CanExecuteChanged(object? sender, EventArgs e)
    {
        UpdateDisableAndEnableRoutesVisibility();
    }

    private void DisableRoutes_Executed(object? sender, EventArgs e)
    {
        UpdateDisableAndEnableRoutesVisibility();
    }

    private void EnableRoutes_CanExecuteChanged(object? sender, EventArgs e)
    {
        UpdateDisableAndEnableRoutesVisibility();
    }

    private void EnableRoutes_Executed(object? sender, EventArgs e)
    {
        UpdateDisableAndEnableRoutesVisibility();
    }

    private void UpdateDisableAndEnableRoutesVisibility()
    {
        if (DisableRoutes is not null && DisableRoutes.CanExecute(null))
        {
            DisableRoutesVisibility = Visibility.Visible;
        }
        else
        {
            DisableRoutesVisibility = Visibility.Collapsed;
        }

        if (EnableRoutes is not null && EnableRoutes.CanExecute(null))
        {
            EnableRoutesVisibility = Visibility.Visible;
        }
        else
        {
            EnableRoutesVisibility = Visibility.Collapsed;
        }

        OnPropertyChanged(nameof(DisableRoutesVisibility));
        OnPropertyChanged(nameof(EnableRoutesVisibility));
    }

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        if (_httpServer != null)
        {
            if (_httpServer.State == HttpServerState.Started)
            {
                StartHttpServerVisibility = Visibility.Collapsed;
                StopHttpServerVisibility = Visibility.Visible;
            }

            if (_httpServer.State == HttpServerState.Starting)
            {
                StartHttpServerVisibility = Visibility.Collapsed;
                StopHttpServerVisibility = Visibility.Collapsed;
            }

            if (_httpServer.State == HttpServerState.Stopping)
            {
                StartHttpServerVisibility = Visibility.Collapsed;
                StopHttpServerVisibility = Visibility.Collapsed;
            }

            if (_httpServer.State == HttpServerState.Stopped)
            {
                StartHttpServerVisibility = Visibility.Visible;
                StopHttpServerVisibility = Visibility.Collapsed;
            }

            OnPropertyChanged(nameof(StartHttpServerVisibility));
            OnPropertyChanged(nameof(StopHttpServerVisibility));

            foreach (var interaction in _httpServer.HandledInteractions.PopAll())
            {
                HandledRequests.Insert(0, interaction);
            }

            foreach (var interaction in _httpServer.UnhandledInteractions.PopAll())
            {
                UnhandledRequests.Insert(0, interaction);
            }
        }
    }
}