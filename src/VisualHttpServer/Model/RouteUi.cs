using System.ComponentModel;
using System.Runtime.CompilerServices;
using VisualHttpServer.Core;

namespace VisualHttpServer.Model;

public class RouteUi : INotifyPropertyChanged
{
    public required string? Method { get; set; }

    public required string? Path { get; set; }

    public required ResponseUi? Response { get; set; }

    public required bool Enabled { get; set; }

    public bool Disabled => !Enabled;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Route ToServerRoute()
    {
        return new Route
        {
            Method = Method!,
            Path = Path!,
            Response = Response!.ToServerResponse(),
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

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}