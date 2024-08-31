namespace VisualHttpServer.Core;

public class ResponseStatus(int code, string reasonPhrase)
{
    public ResponseStatus(int code) : this(code, string.Empty)
    {
    }

    public int Code { get; } = code;
    public string ReasonPhrase { get; } = reasonPhrase;
}