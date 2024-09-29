using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VisualHttpServer.Commands;
using VisualHttpServer.Configuration;
using VisualHttpServer.Core;
using VisualHttpServer.Core.Configuration;
using VisualHttpServer.Model;
using VisualHttpServer.Services;

namespace VisualHttpServer;

internal static class ServiceLocator
{
    private static ServiceProvider? _serviceProvider;

    public static void Init(IConfiguration configuration)
    {
        if (_serviceProvider is not null)
        {
            throw new InvalidOperationException("ServiceLocator already initialized.");
        }

        var serviceCollection = new ServiceCollection();

        ConfigureServices(serviceCollection, configuration);

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

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpServer, HttpServer>();
        services.AddSingleton<IConfig>(new Config(configuration));
        services.AddSingleton<ResponseStatusCollection>();
        services.AddSingleton<IMessageViewer, MessageViewer>();
        services.AddSingleton<RouteUiCollection>();
        services.AddSingleton<StartHttpServerCommand>();
        services.AddSingleton<StopHttpServerCommand>();
        services.AddSingleton<EditRouteCommand>();
        services.AddSingleton<DuplicateRouteCommand>();
        services.AddSingleton<DisableRoutesCommand>();
        services.AddSingleton<EnableRoutesCommand>();
        services.AddSingleton<RemoveRoutesCommand>();
        services.AddSingleton<ClearRoutesCommand>();
        services.AddSingleton<SaveRouteCommand>();
        services.AddSingleton<OpenResponseCommand>();
    }
}