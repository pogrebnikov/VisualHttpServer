﻿using VisualHttpServer.Core;

namespace VisualHttpServer.Model;

internal class ResponseUi
{
    public int StatusCode { get; set; }

    public string? Body { get; set; }

    public Response ToServerResponse()
    {
        return new Response
        {
            StatusCode = StatusCode,
            Body = Body
        };
    }
}