using VisualHttpServer.Model;

namespace VisualHttpServer.Abstractions;

internal interface IRoutesView
{
    int SelectedRoutesCount { get; }
    IReadOnlyCollection<RouteUi> SelectedRoutes { get; }
    event EventHandler RoutesSelectionChanged;
}