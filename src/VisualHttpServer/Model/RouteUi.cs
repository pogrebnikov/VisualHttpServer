using System.ComponentModel;
using VisualHttpServer.Core;

namespace VisualHttpServer.Model;

internal class RouteUi : INotifyPropertyChanged
{
    private string? _method;
    private string? _path;

    public required string? Method
    {
        get => _method;
        set
        {
            var changed = _method != value;
            _method = value;

            if (changed)
            {
                OnPropertyChanged(nameof(Method));
            }
        }
    }

    public required string? Path
    {
        get => _path;
        set
        {
            var changed = _path != value;
            _path = value;

            if (changed)
            {
                OnPropertyChanged(nameof(Path));
            }
        }
    }

    public required ResponseUi? Response { get; set; }

    public required bool Enabled { get; set; }

    public bool Disabled => !Enabled;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Route ToServerRoute(ResponseStatusCollection responseStatuses)
    {
        return new Route
        {
            Method = Method!,
            Path = Path!,
            Response = Response!.ToServerResponse(responseStatuses),
            Enabled = Enabled
        };
    }

    public void Disable()
    {
        Enabled = false;
        OnPropertyChanged(nameof(Enabled));
    }

    public void Enable()
    {
        Enabled = true;
        OnPropertyChanged(nameof(Enabled));
    }

    public void Update(RouteUi sourceRoute)
    {
        Method = sourceRoute.Method;
        Path = sourceRoute.Path;
        Response!.Update(sourceRoute.Response!);
    }

    public RouteUi Clone()
    {
        return new RouteUi
        {
            Method = Method,
            Path = Path,
            Response = Response!.Clone(),
            Enabled = Enabled
        };
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}