using System.Runtime.CompilerServices;
using InvestTracker.Notifications.Api.Permissions;
using InvestTracker.Notifications.Core;
using InvestTracker.Notifications.Infrastructure;
using InvestTracker.Notifications.Infrastructure.Hubs;
using InvestTracker.Shared.Abstractions.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Bootstrapper")]
namespace InvestTracker.Notifications.Api;

internal static class Extensions
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection services)
    {
        services
            .AddCore()
            .AddInfrastructure()
            .AddSingleton<IModulePermissionMatrix, NotificationsPermissionMatrix>();

        return services;
    }
    
    public static WebApplication UseNotificationsModule(this WebApplication app)
    {
        app.MapHub<NotificationHub>("/notification-hub");
        return app;
    }
}