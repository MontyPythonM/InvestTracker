using System.Reflection;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.IntegrationEvents;

internal static class Extensions
{
    public static IServiceCollection AddIntegrationEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}