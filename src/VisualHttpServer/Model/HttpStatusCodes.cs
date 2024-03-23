namespace VisualHttpServer.Model;

internal static class HttpStatusCodes
{
    public const int Ok = 200;

    public static IEnumerable<int> All
    {
        get { return new[] { Ok, 400, 401, 403, 404, 500 }; }
    }
}