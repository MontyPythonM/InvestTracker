using System.Runtime.CompilerServices;
using InvestTracker.Notifications.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Bootstrapper")]
namespace InvestTracker.Notifications.Api;

internal static class Extensions
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection services)
    {
        services.AddCore();
        
        return services;
    }
}