using VisualHttpServer.Core;

namespace VisualHttpServer.Model;

internal class RouteUi
{
    public string? Method { get; set; }

    public string? Path { get; set; }

    public ResponseUi? Response { get; set; }

    public Route ToServerRoute()
    {
        return new Route
        {
            Method = Method!,
            Path = Path!,
            Response = Response!.ToServerResponse()
        };
    }
}