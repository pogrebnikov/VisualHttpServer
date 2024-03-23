namespace VisualHttpServer.Core;

public class Route
{
    public required string Method { get; init; }

    public required string Path { get; init; }

    public required Response Response { get; init; }
}