using System.Reflection;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using InvestTracker.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InvestTracker.Shared.Infrastructure.Modules;

public static class Extensions
{
    internal static IHostBuilder ConfigureModuleAppSettings(this IHostBuilder builder, IEnumerable<Assembly> assemblies)
        => builder.ConfigureAppConfiguration((context, configuration) =>
        {
            foreach (var settings in GetSettings("*"))
            {
                configuration.AddJsonFile(settings);
            }

            foreach (var settings in GetSettings($"*.{context.HostingEnvironment.EnvironmentName}"))
            {
                configuration.AddJsonFile(settings);
            }

            foreach (var modules in GetModuleEntryPointTypes(assemblies))
            {
                configuration.AddUserSecrets(modules.Assembly);
            }
            
            IEnumerable<string> GetSettings(string pattern)
                => Directory.EnumerateFiles(context.HostingEnvironment.ContentRootPath,
                    $"module.{pattern}.json", SearchOption.AllDirectories);
        });
    
    internal static IServiceCollection AddModuleRequests(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddModuleRegistry(assemblies);
        services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();
        services.AddSingleton<IModuleClient, ModuleClient>();
        services.AddSingleton<IModuleSubscriber, ModuleSubscriber>();

        return services;
    }

    public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
        => app.ApplicationServices.GetRequiredService<IModuleSubscriber>();
    
    private static void AddModuleRegistry(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var registry = new ModuleRegistry();
        
        // get all event types from assemblies
        var types = assemblies.SelectMany(assembly => assembly.GetTypes()).ToArray();
        var eventTypes = types
            .Where(type => type.IsClass && typeof(IEvent).IsAssignableFrom(type))
            .ToArray();
        
        // find and use living instance of event dispatcher according of event type
        services.AddSingleton<IModuleRegistry>(serviceProvider =>
        {
            var eventDispatcher = serviceProvider.GetRequiredService<IEventDispatcher>();
            var eventDispatcherType = eventDispatcher.GetType();
            
            foreach (var type in eventTypes)
            {
                registry.AddBroadcastAction(type, @event =>
                    ((Task) eventDispatcherType.GetMethod(nameof(eventDispatcher.PublishAsync))?
                        .MakeGenericMethod(type)
                        .Invoke(eventDispatcher, new[] {@event})!));
            }

            return registry;
        });
    }
    
    private static IEnumerable<Type> GetModuleEntryPointTypes(IEnumerable<Assembly> assemblies)
    {
        var moduleEntryPointTypes = new List<Type>();
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes()
                .Where(t => typeof(IModule).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
            
            moduleEntryPointTypes.AddRange(types);
        }

        return moduleEntryPointTypes;
    }
}