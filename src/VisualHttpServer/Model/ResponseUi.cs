using System.ComponentModel;
using System.IO;
using System.Text.Json;
using VisualHttpServer.Core;

namespace VisualHttpServer.Model;

internal class ResponseUi : INotifyPropertyChanged
{
    private string _body = string.Empty;
    private string _reasonPhrase = string.Empty;
    private int _statusCode;

    public required int StatusCode
    {
        get => _statusCode;
        set
        {
            var changed = _statusCode != value;
            _statusCode = value;

            if (changed)
            {
                OnPropertyChanged(nameof(StatusCode));
            }
        }
    }

    public required string ReasonPhrase
    {
        get => _reasonPhrase;
        set
        {
            var changed = _reasonPhrase != value;
            _reasonPhrase = value;

            if (changed)
            {
                OnPropertyChanged(nameof(ReasonPhrase));
            }
        }
    }

    public string StatusCodeWithReasonPhrase => $"{StatusCode} {ReasonPhrase}";

    public required string Body
    {
        get => _body;
        set
        {
            var changed = _body != value;
            _body = value;

            if (changed)
            {
                OnPropertyChanged(nameof(Body));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Response ToServerResponse(ResponseStatusCollection responseStatuses)
    {
        var responseStatus = responseStatuses.GetOrCreate(StatusCode);

        return new Response
        {
            Status = responseStatus,
            Body = Body
        };
    }

    public void Update(ResponseUi source)
    {
        StatusCode = source.StatusCode;
        ReasonPhrase = source.ReasonPhrase;
        Body = source.Body;
    }

    public ResponseUi Clone()
    {
        return new ResponseUi
        {
            StatusCode = StatusCode,
            ReasonPhrase = ReasonPhrase,
            Body = Body
        };
    }

    public async Task ReadFromFileAsync(string fileName)
    {
        var jsonStream = File.OpenRead(fileName);
        var jsonModel = await JsonSerializer.DeserializeAsync<ResponseJsonModel>(jsonStream);
        if (jsonModel is null)
        {
            return;
        }

        StatusCode = jsonModel.StatusCode;
        ReasonPhrase = jsonModel.ReasonPhrase ?? string.Empty;
        Body = jsonModel.Body ?? string.Empty;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private class ResponseJsonModel
    {
        public int StatusCode { get; init; }
        public string? ReasonPhrase { get; init; }
        public string? Body { get; init; }
    }
}