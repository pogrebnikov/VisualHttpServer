using VisualHttpServer.Core.Configuration;

namespace VisualHttpServer.Configuration;

internal class HttpStatusCodeConfig : IHttpStatusCodeConfig
{
    public required int Code { get; init; }
    public required string ReasonPhrase { get; init; }
}