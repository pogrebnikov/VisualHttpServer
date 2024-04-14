namespace VisualHttpServer.Abstractions;

internal interface IRoutesView
{
    int SelectedRoutesCount { get; }
    event EventHandler RoutesSelectionChanged;
}