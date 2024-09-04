namespace VisualHttpServer.Model;

internal class InteractionUi
{
    public required TimeSpan Time { get; init; }

    public required string Method { get; init; }

    public required int StatusCode { get; init; }

    public required string ReasonPhrase { get; init; }

    public string StatusCodeWithReasonPhrase => $"{StatusCode} {ReasonPhrase}";

    public required string Path { get; init; }
}