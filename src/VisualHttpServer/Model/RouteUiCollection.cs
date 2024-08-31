using System.Collections.ObjectModel;
using VisualHttpServer.Core;

namespace VisualHttpServer.Model;

internal class RouteUiCollection(IHttpServer httpServer, ResponseStatusCollection responseStatuses)
{
    private readonly ObservableCollection<RouteUi> _collection = new();

    public ObservableCollection<RouteUi> AsObservable()
    {
        return _collection;
    }

    public void Add(RouteUi route)
    {
        _collection.Add(route);
        UpdateServerRoutes();
    }

    public void RemoveRange(IEnumerable<RouteUi> routes)
    {
        foreach (var route in routes)
        {
            _collection.Remove(route);
        }

        UpdateServerRoutes();
    }

    public void Clear()
    {
        _collection.Clear();
        UpdateServerRoutes();
    }

    public bool Contains(RouteUi route)
    {
        return _collection.Any(rt => rt.Method == route.Method && rt.Path == route.Path);
    }

    public void Update()
    {
        UpdateServerRoutes();
    }

    private void UpdateServerRoutes()
    {
        var routes = _collection.Select(route => route.ToServerRoute(responseStatuses)).ToArray();

        httpServer.Routes.Update(routes);
    }
}