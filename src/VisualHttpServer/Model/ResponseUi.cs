using VisualHttpServer.Core;

namespace VisualHttpServer.Model;

public class ResponseUi
{
    public int StatusCode { get; set; }

    public string? Body { get; set; }

    public Response ToServerResponse()
    {
        return new Response
        {
            StatusCode = StatusCode,
            Body = Body
        };
    }

    public void Update(ResponseUi source)
    {
        StatusCode = source.StatusCode;
        Body = source.Body;
    }

    public ResponseUi Clone()
    {
        return new ResponseUi
        {
            StatusCode = StatusCode,
            Body = Body
        };
    }
}