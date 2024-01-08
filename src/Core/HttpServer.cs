using System.Net;
using System.Net.Sockets;

namespace VisualHttpServer.Core;

public class HttpServer : IHttpServer
{
    private TcpListener? _listener;

    public InteractionCollection UnhandledInteractions { get; } = new();

    public void Start(IPAddress address, int port)
    {
        _listener = new TcpListener(address, port);
        _listener.Start();

        Task.Run(ProcessRequests);
    }

    private void ProcessRequests()
    {
        if (_listener == null)
        {
            return;
        }

        while (true)
        {
            TcpClient? client = null;
            try
            {
                client = _listener.AcceptTcpClient();

                using var stream = client.GetStream();

                stream.ReadTimeout = 1000;
                var request = Request.Read(stream);

                if (request == null)
                {
                    continue;
                }

                Response response = new()
                {
                    StatusCode = 404
                };

                response.Write(stream);

                Interaction interaction = new()
                {
                    Request = request,
                    Response = response
                };

                UnhandledInteractions.Add(interaction);
            }
            finally
            {
                client?.Dispose();
            }
        }
    }
}