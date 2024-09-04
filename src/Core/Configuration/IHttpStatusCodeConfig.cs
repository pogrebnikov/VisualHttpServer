namespace VisualHttpServer.Core.Configuration;

public interface IHttpStatusCodeConfig
{
    int Code { get; }
    string ReasonPhrase { get; }
}