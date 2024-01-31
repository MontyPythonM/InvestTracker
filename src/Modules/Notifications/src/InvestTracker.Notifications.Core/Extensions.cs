using System.Runtime.CompilerServices;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Notifications.Core.Options;
using InvestTracker.Notifications.Core.Persistence;
using InvestTracker.Notifications.Core.Persistence.Repositories;
using InvestTracker.Notifications.Core.Services;
using InvestTracker.Shared.Infrastructure;
using InvestTracker.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Notifications.Api")]
namespace InvestTracker.Notifications.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        var notificationServiceOptions = services
            .GetOptions<NotificationServiceOptions>("Notifications:NotificationService");
        
        services
            .AddSingleton<INotificationPublisher, NotificationService>()
            .AddHostedService(sp => (NotificationService)sp.GetService<INotificationPublisher>()!)
            .AddSingleton(notificationServiceOptions)
            .AddSignalR(s => s.EnableDetailedErrors = notificationServiceOptions.EnableDetailedErrorsSignalR);

        services
            .AddPostgres<NotificationsDbContext>()
            .AddScoped<IReceiverRepository, ReceiverRepository>()
            .AddScoped<IGlobalNotificationSetupRepository, GlobalNotificationSetupRepository>();

        return services;
    }
}