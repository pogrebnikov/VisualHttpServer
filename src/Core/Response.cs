using System.Text;

namespace VisualHttpServer.Core;

public class Response
{
    private const string EndOfLine = "\r\n";
    public required int StatusCode { get; init; }

    public required string? Body { get; init; }

    public void Write(Stream stream)
    {
        var responseText = GetResponseText();
        var data = Encoding.Default.GetBytes(responseText);
        stream.Write(data, 0, data.Length);
    }

    private string GetResponseText()
    {
        StringBuilder sb = new($"HTTP/1.1 {StatusCode} OK{EndOfLine}");
        var body = Body ?? string.Empty;
        sb.Append($"Content-Length: {body.Length}{EndOfLine}{EndOfLine}");
        if (!string.IsNullOrEmpty(body))
        {
            sb.Append(body + EndOfLine);
        }

        return sb.ToString();
    }
}