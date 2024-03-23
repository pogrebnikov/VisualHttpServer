namespace VisualHttpServer.Model;

public class HttpMethods
{
    public const string Get = "GET";

    public static IEnumerable<string> All
    {
        get { return new[] { Get, "POST", "PUT", "DELETE" }; }
    }
}