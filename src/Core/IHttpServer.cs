using System.Net;

namespace VisualHttpServer.Core;

public interface IHttpServer
{
    InteractionCollection UnhandledInteractions { get; }

    void Start(IPAddress address, int port);
}