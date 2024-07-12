using System.Windows.Input;
using VisualHttpServer.Abstractions;
using VisualHttpServer.Model;
using VisualHttpServer.Windows;

namespace VisualHttpServer.Commands;

internal class EditRouteCommand : ICommand
{
    private bool _canExecute;
    private IRoutesView? _routesView;

    public EditRouteCommand()
    {
        _canExecute = GetCanExecute();
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute;
    }

    public void Execute(object? parameter)
    {
        if (_routesView is null)
        {
            throw new InvalidOperationException($"{nameof(_routesView)} is null.");
        }

        if (parameter is not RouteUi route)
        {
            return;
        }

        EditRouteWindow window = new();
        var viewModel = (EditRouteWindowViewModel)window.DataContext;

        viewModel.SetRoutesView(_routesView);
        viewModel.SetEditedRoute(route);
        viewModel.SetCloseEditWindowAction(window.Close);

        window.ShowDialog();
    }

    public event EventHandler? CanExecuteChanged;

    public void SetRoutesView(IRoutesView routesView)
    {
        _routesView = routesView;
        _routesView.RoutesSelectionChanged += RoutesView_RoutesSelectionChanged;
    }

    private void RoutesView_RoutesSelectionChanged(object? sender, EventArgs e)
    {
        var canExecute = GetCanExecute();
        if (_canExecute == canExecute)
        {
            return;
        }

        _canExecute = canExecute;
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    private bool GetCanExecute()
    {
        if (_routesView is null)
        {
            return false;
        }

        return _routesView.SelectedRoutesCount == 1;
    }
}