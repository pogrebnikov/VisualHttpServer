using System.Net;
using System.Net.Sockets;

namespace VisualHttpServer.Core;

public class HttpServer(ResponseStatusCollection responseStatuses) : IHttpServer
{
    private TcpListener? _listener;

    private readonly Response _response404 = new()
    {
        Status = responseStatuses.NotFound,
        Body = null
    };

    private readonly Response _response500 = new()
    {
        Status = responseStatuses.InternalServerError,
        Body = "The route is disabled."
    };

    public HttpServerState State { get; private set; } = HttpServerState.Stopped;
    public RouteCollection Routes { get; } = new();
    public InteractionCollection HandledInteractions { get; } = new();
    public InteractionCollection UnhandledInteractions { get; } = new();

    public void Start(IPAddress address, int port)
    {
        State = HttpServerState.Starting;
        _listener = new TcpListener(address, port);
        _listener.Start();

        Task.Factory.StartNew(ProcessRequests, TaskCreationOptions.LongRunning);
    }

    public void Stop()
    {
        State = HttpServerState.Stopping;
        try
        {
            _listener?.Stop();
        }
        finally
        {
            State = HttpServerState.Stopped;
        }
    }

    private void ProcessRequests()
    {
        if (_listener == null)
        {
            return;
        }

        State = HttpServerState.Started;

        try
        {
            while (true)
            {
                TcpClient? client = null;
                try
                {
                    try
                    {
                        client = _listener.AcceptTcpClient();
                    }
                    catch (SocketException)
                    {
                        return;
                    }

                    using var stream = client.GetStream();

                    stream.ReadTimeout = 1000;
                    var request = Request.Read(stream);

                    if (request == null)
                    {
                        continue;
                    }

                    var route = Routes.Find(request.Method, request.Path).FirstOrDefault();

                    bool handled;
                    Response response;
                    if (route is null)
                    {
                        handled = false;
                        response = _response404;
                    }
                    else
                    {
                        handled = true;
                        response = route.Enabled ? route.Response : _response500;
                    }

                    response.Write(stream);

                    Interaction interaction = new()
                    {
                        Request = request,
                        Response = response
                    };

                    if (handled)
                    {
                        HandledInteractions.Add(interaction);
                    }
                    else
                    {
                        UnhandledInteractions.Add(interaction);
                    }
                }
                finally
                {
                    client?.Dispose();
                }
            }
        }
        finally
        {
            if (State is HttpServerState.Starting or HttpServerState.Started)
            {
                Stop();
            }
        }
    }
}