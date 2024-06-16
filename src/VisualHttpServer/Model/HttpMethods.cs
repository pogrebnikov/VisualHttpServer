namespace VisualHttpServer.Model;

public class HttpMethods
{
    public const string Get = "GET";

    public static IEnumerable<string> All =>
        [Get, "HEAD", "POST", "PUT", "DELETE", "CONNECT", "OPTIONS", "TRACE", "PATCH"];
}