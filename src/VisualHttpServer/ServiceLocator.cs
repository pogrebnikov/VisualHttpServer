using Microsoft.Extensions.DependencyInjection;
using VisualHttpServer.Commands;
using VisualHttpServer.Core;
using VisualHttpServer.Model;
using VisualHttpServer.Services;

namespace VisualHttpServer;

internal static class ServiceLocator
{
    private static ServiceProvider? _serviceProvider;

    public static void Init()
    {
        if (_serviceProvider is not null)
        {
            throw new InvalidOperationException("ServiceLocator already initialized.");
        }

        var serviceCollection = new ServiceCollection();

        ConfigureServices(serviceCollection);

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public static T? Resolve<T>() where T : class
    {
        if (_serviceProvider is null)
        {
            return default;
        }

        var serviceType = typeof(T);
        
        if (_serviceProvider.GetService(serviceType) is not T service)
        {
            throw new InvalidOperationException($"Can't resolve service. Type is '{serviceType}'");
        }

        return service;
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IHttpServer, HttpServer>();
        services.AddSingleton<IMessageViewer, MessageViewer>();
        services.AddSingleton<RouteUiCollection>();
        services.AddSingleton<StartHttpServerCommand>();
        services.AddSingleton<StopHttpServerCommand>();
        services.AddSingleton<ClearRoutesCommand>();
        services.AddSingleton<RemoveRoutesCommand>();
    }
}