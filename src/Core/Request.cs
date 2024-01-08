using System.Net.Sockets;
using System.Text;

namespace VisualHttpServer.Core;

public class Request
{
    private Request()
    {
        Time = DateTime.Now.TimeOfDay;
    }

    public TimeSpan Time { get; }

    public required string Method { get; init; }

    public required string Path { get; init; }

    public static Request? Read(NetworkStream stream)
    {
        var content = GetRequestContent(stream);
        return Parse(content);
    }

    private static Request? Parse(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return null;
        }

        try
        {
            var parts = s.Split(" ");

            return new Request
            {
                Method = parts[0],
                Path = parts[1]
            };
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error a request parsing: {Environment.NewLine}{s}", e);
        }
    }

    private static string GetRequestContent(NetworkStream networkStream)
    {
        var buffer = new byte[1024];
        StringBuilder content = new();

        while (networkStream.DataAvailable)
        {
            var count = networkStream.Read(buffer, 0, buffer.Length);
            content.Append(Encoding.Default.GetString(buffer, 0, count));
        }

        return content.ToString();
    }
}