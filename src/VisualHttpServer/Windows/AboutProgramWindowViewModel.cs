using System.ComponentModel;

namespace VisualHttpServer.Windows;

internal class AboutProgramWindowViewModel : INotifyPropertyChanged
{
    public string Version => "2024.1";

    public string Author => "Alexei Pogrebnikov";

    public event PropertyChangedEventHandler? PropertyChanged;
}