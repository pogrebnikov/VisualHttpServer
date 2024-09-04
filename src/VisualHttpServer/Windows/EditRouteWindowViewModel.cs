using System.ComponentModel;
using VisualHttpServer.Abstractions;
using VisualHttpServer.Commands;
using VisualHttpServer.Core;
using VisualHttpServer.Model;

namespace VisualHttpServer.Windows;

internal class EditRouteWindowViewModel : INotifyPropertyChanged
{
    private RouteUi? _route;

    public SaveRouteCommand? SaveRoute { get; } = ServiceLocator.Resolve<SaveRouteCommand>();

    public RouteUi? Route
    {
        get => _route;
        set
        {
            _route = value;
            OnPropertyChanged(nameof(Route));

            if (_route is not null)
            {
                _route.PropertyChanged += Route_PropertyChanged;
                _route.Response!.PropertyChanged += RouteResponse_PropertyChanged;

                ValidateMethod();
                ValidateStatusCode();
            }
        }
    }

    public string? MethodWarning { get; set; }

    public string? StatusCodeWarning { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

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


    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Route_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        ValidateMethod();
    }

    private void RouteResponse_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        ValidateStatusCode();
    }

    private void ValidateMethod()
    {
        var method = Route!.Method;

        if (!string.IsNullOrEmpty(method) && !HttpMethods.All.Contains(method))
        {
            MethodWarning = $"Warning: '{method}' not in the list of HTTP methods!";
            OnPropertyChanged(nameof(MethodWarning));
        }
        else
        {
            if (!string.IsNullOrEmpty(MethodWarning))
            {
                MethodWarning = string.Empty;
                OnPropertyChanged(nameof(MethodWarning));
            }
        }
    }

    private void ValidateStatusCode()
    {
        var statusCode = Route!.Response!.StatusCode;

        var responseStatuses = ServiceLocator.Resolve<ResponseStatusCollection>();
        var responseStatus = responseStatuses!.Get(statusCode);

        if (responseStatus is null)
        {
            Route.Response.ReasonPhrase = string.Empty;
            
            StatusCodeWarning = $"Warning: '{statusCode}' not in the list of HTTP status codes!";
            OnPropertyChanged(nameof(StatusCodeWarning));
        }
        else
        {
            Route.Response.ReasonPhrase = responseStatus.ReasonPhrase;

            if (!string.IsNullOrEmpty(StatusCodeWarning))
            {
                StatusCodeWarning = string.Empty;
                OnPropertyChanged(nameof(StatusCodeWarning));
            }
        }
    }
}