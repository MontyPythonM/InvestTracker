using System.Runtime.CompilerServices;
using InvestTracker.Notifications.Core;
using InvestTracker.Notifications.Core.Hubs;
using Microsoft.AspNetCore.Builder;
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
    
    internal static WebApplication UseNotificationsModule(this WebApplication app)
    {
        var method = nameof(AdministratorHub.SendPushNotification);
        app.MapHub<AdministratorHub>("administrator");

        return app;
    }
}