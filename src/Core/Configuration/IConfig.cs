namespace VisualHttpServer.Core.Configuration;

public interface IConfig
{
    IReadOnlyCollection<IHttpStatusCodeConfig> HttpStatusCodes { get; }
}