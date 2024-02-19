using System.Collections;
using System.Collections.ObjectModel;

namespace VisualHttpServer.Model;

internal class RouteUiCollection : IEnumerable<RouteUi>
{
    private readonly ObservableCollection<RouteUi> _collection = new();

    public IEnumerator<RouteUi> GetEnumerator()
    {
        return _collection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    internal ObservableCollection<RouteUi> AsObservable()
    {
        return _collection;
    }

    internal void Add(RouteUi route)
    {
        _collection.Add(route);
    }
}