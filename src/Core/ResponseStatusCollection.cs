using VisualHttpServer.Core.Configuration;

namespace VisualHttpServer.Core;

public class ResponseStatusCollection(IConfig config)
{
    public ResponseStatus Ok => GetOrCreate(200);
    public ResponseStatus NotFound => GetOrCreate(404);
    public ResponseStatus InternalServerError => GetOrCreate(500);

    public ResponseStatus? Get(int statusCode)
    {
        var statusCodeConfig = config.HttpStatusCodes.FirstOrDefault(sc => sc.Code == statusCode);

        return statusCodeConfig is null ? null : new ResponseStatus(statusCode, statusCodeConfig.ReasonPhrase);
    }

    public ResponseStatus GetOrCreate(int statusCode)
    {
        var responseStatus = Get(statusCode);

        return responseStatus ?? new ResponseStatus(statusCode);
    }
}