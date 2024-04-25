using System.ComponentModel;
using VisualHttpServer.Commands;
using VisualHttpServer.Model;
using VisualHttpServer.Services;

namespace VisualHttpServer.Windows;

internal class NewRouteWindowViewModel : INotifyPropertyChanged
{
    public NewRouteWindowViewModel()
    {
        var routes = ServiceLocator.Resolve<RouteUiCollection>();
        var messageViewer = ServiceLocator.Resolve<IMessageViewer>();

        if (routes is not null && messageViewer is not null)
        {
            CreateRoute = new CreateRouteCommand(routes, messageViewer);
        }
    }

    public CreateRouteCommand? CreateRoute { get; }

    public RouteUi Route { get; } = new()
    {
        Method = HttpMethods.Get,
        Path = "/",
        Response = new ResponseUi
        {
            StatusCode = HttpStatusCodes.Ok
        },
        Enabled = true
    };

    public IEnumerable<string> Methods => HttpMethods.All;

    public IEnumerable<int> StatusCodes => HttpStatusCodes.All;
    public event PropertyChangedEventHandler? PropertyChanged;

    public void SetCloseWindowAction(Action action)
    {
        if (CreateRoute is not null)
        {
            CreateRoute.CloseWindowAction = action;
        }
    }
}