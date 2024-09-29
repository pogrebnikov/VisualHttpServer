using System.ComponentModel;
using VisualHttpServer.Commands;
using VisualHttpServer.Core;
using VisualHttpServer.Model;
using VisualHttpServer.Services;

namespace VisualHttpServer.Windows;

internal class NewRouteWindowViewModel : INotifyPropertyChanged
{
    public NewRouteWindowViewModel()
    {
        var responseStatuses = ServiceLocator.Resolve<ResponseStatusCollection>();

        if (responseStatuses is not null)
        {
            var responseStatus = responseStatuses.Ok;

            Route = new RouteUi
            {
                Method = HttpMethods.Get,
                Path = "/",
                Response = new ResponseUi
                {
                    StatusCode = responseStatus.Code,
                    ReasonPhrase = responseStatus.ReasonPhrase,
                    Body = string.Empty
                },
                Enabled = true
            };

            Route.PropertyChanged += Route_PropertyChanged;
            Route.Response.PropertyChanged += RouteResponse_PropertyChanged;
        }

        var routes = ServiceLocator.Resolve<RouteUiCollection>();
        var messageViewer = ServiceLocator.Resolve<IMessageViewer>();

        if (routes is not null && messageViewer is not null)
        {
            CreateRoute = new CreateRouteCommand(routes, messageViewer);
        }
    }

    public OpenResponseCommand? OpenResponse { get; } = ServiceLocator.Resolve<OpenResponseCommand>();

    public CreateRouteCommand? CreateRoute { get; }

    public RouteUi? Route { get; }

    public string? MethodWarning { get; set; }

    public string? StatusCodeWarning { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void SetCloseWindowAction(Action action)
    {
        if (CreateRoute is not null)
        {
            CreateRoute.CloseWindowAction = action;
        }
    }

    private void Route_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Route.Method))
        {
            ValidateMethod();
        }
    }

    private void RouteResponse_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Route.Response.StatusCode))
        {
            ValidateStatusCode();
            UpdateReasonPhrase();
        }
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
            StatusCodeWarning = $"Warning: '{statusCode}' not in the list of HTTP status codes!";
            OnPropertyChanged(nameof(StatusCodeWarning));
        }
        else
        {
            if (string.IsNullOrEmpty(StatusCodeWarning))
            {
                return;
            }

            StatusCodeWarning = string.Empty;
            OnPropertyChanged(nameof(StatusCodeWarning));
        }
    }

    private void UpdateReasonPhrase()
    {
        var statusCode = Route!.Response!.StatusCode;

        var responseStatuses = ServiceLocator.Resolve<ResponseStatusCollection>();
        var responseStatus = responseStatuses!.Get(statusCode);

        Route.Response.ReasonPhrase = responseStatus is null ? string.Empty : responseStatus.ReasonPhrase;
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}