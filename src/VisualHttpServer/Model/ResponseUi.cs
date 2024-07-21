using System.ComponentModel;
using VisualHttpServer.Core;

namespace VisualHttpServer.Model;

public class ResponseUi : INotifyPropertyChanged
{
    private int _statusCode;
    private string? _body;

    public int StatusCode
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

    public string? Body
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

    public Response ToServerResponse()
    {
        return new Response
        {
            StatusCode = StatusCode,
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
            Body = Body
        };
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}