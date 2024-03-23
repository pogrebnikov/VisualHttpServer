using System.Windows;

namespace VisualHttpServer;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        ServiceLocator.Init();
    }
}