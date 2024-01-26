using System.Net;

namespace VisualHttpServer.Core;

public interface IHttpServer
{
    HttpServerState State { get; }
    InteractionCollection UnhandledInteractions { get; }

    void Start(IPAddress address, int port);
    void Stop();
}