using System.ComponentModel;
using VisualHttpServer.Core;

namespace VisualHttpServer.Model;

internal class ResponseUi : INotifyPropertyChanged
{
    private int _statusCode;
    private string _reasonPhrase = string.Empty;
    private string _body = string.Empty;

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

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}