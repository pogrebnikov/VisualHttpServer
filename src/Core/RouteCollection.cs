﻿using System.Collections;

namespace VisualHttpServer.Core;

public class RouteCollection : IEnumerable<Route>
{
    private IReadOnlyCollection<Route> _routes = Array.Empty<Route>();

    public IEnumerator<Route> GetEnumerator()
    {
        return _routes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Update(Route[] routes)
    {
        foreach (var route in routes)
        {
            CheckRoute(route);
        }

        Interlocked.Exchange(ref _routes, routes);
    }

    public IEnumerable<Route> Find(string method, string path)
    {
        return _routes.Where(m => m.Method == method && m.Path == path).ToArray();
    }

    private static void CheckRoute(Route route)
    {
        if (route == null)
        {
            throw new ArgumentNullException(nameof(route));
        }

        if (string.IsNullOrEmpty(route.Method))
        {
            throw new ArgumentException("Method of the route is null or empty.");
        }

        if (string.IsNullOrEmpty(route.Path))
        {
            throw new ArgumentException("Path of the route is null or empty.");
        }

        if (route.Response.Status.Code == 0)
        {
            throw new ArgumentException("StatusCode of the response is 0.");
        }
    }
}