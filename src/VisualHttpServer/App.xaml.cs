using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;

namespace VisualHttpServer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true);

        var configuration = builder.Build();

        ServiceLocator.Init(configuration);
    }
}