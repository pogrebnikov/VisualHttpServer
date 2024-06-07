using System.ComponentModel;
using VisualHttpServer.Abstractions;
using VisualHttpServer.Commands;
using VisualHttpServer.Model;

namespace VisualHttpServer.Windows;

internal class EditRouteWindowViewModel : INotifyPropertyChanged
{
    private RouteUi? _route;

    public event PropertyChangedEventHandler? PropertyChanged;

    public SaveRouteCommand? SaveRoute { get; } = ServiceLocator.Resolve<SaveRouteCommand>();

    public RouteUi? Route
    {
        get => _route;
        set
        {
            _route = value;
            OnPropertyChanged(nameof(Route));
        }
    }

    public void SetEditedRoute(RouteUi route)
    {
        if (SaveRoute is not null)
        {
            SaveRoute.SetEditedRoute(route);
            Route = route.Clone();
        }
    }

    public void SetRoutesView(IRoutesView routesView)
    {
        if (SaveRoute is null)
        {
            throw new InvalidOperationException($"{nameof(SaveRoute)} is null.");
        }

        SaveRoute.SetRoutesView(routesView);
    }

    public void SetCloseEditWindowAction(Action closeEditWindowAction)
    {
        if (SaveRoute is null)
        {
            throw new InvalidOperationException($"{nameof(SaveRoute)} is null.");
        }

        SaveRoute.SetCloseEditWindowAction(closeEditWindowAction);
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}