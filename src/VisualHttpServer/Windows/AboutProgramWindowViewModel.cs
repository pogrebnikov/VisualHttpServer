using System.ComponentModel;

namespace VisualHttpServer.Windows;

internal class AboutProgramWindowViewModel : INotifyPropertyChanged
{
    public string Version => "2024.2";

    public string Author => "Alexei Pogrebnikov";

    public event PropertyChangedEventHandler? PropertyChanged;
}