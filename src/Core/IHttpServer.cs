using System.Net;

namespace VisualHttpServer.Core;

public interface IHttpServer
{
    HttpServerState State { get; }
    RouteCollection Routes { get; }
    InteractionCollection UnhandledInteractions { get; }
    InteractionCollection HandledInteractions { get; }

    void Start(IPAddress address, int port);
    void Stop();
}