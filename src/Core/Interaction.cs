namespace VisualHttpServer.Core;

public class Interaction
{
    public required Request Request { get; init; }
    public required Response Response { get; init; }
}