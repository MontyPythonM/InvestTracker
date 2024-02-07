using System.Runtime.CompilerServices;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Notifications.Core.Options;
using InvestTracker.Notifications.Core.Persistence;
using InvestTracker.Notifications.Core.Persistence.Repositories;
using InvestTracker.Notifications.Core.Services.GlobalSettings;
using InvestTracker.Notifications.Core.Services.Notifications;
using InvestTracker.Notifications.Core.Services.Receivers;
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
            .AddPostgres<NotificationsDbContext>()
            .AddScoped<IReceiverRepository, ReceiverRepository>()
            .AddScoped<IReceiverService, ReceiverService>()
            .AddScoped<INotificationPublisher, NotificationPublisher>()
            .AddScoped<IGlobalSettingsRepository, GlobalSettingsRepository>()
            .AddScoped<IGlobalSettingsService, GlobalSettingsService>()
            .AddSingleton<INotificationSender, NotificationSender>()
            .AddHostedService(sp => (NotificationSender)sp.GetService<INotificationSender>()!)
            .AddSingleton(notificationServiceOptions)
            .AddSignalR(s => s.EnableDetailedErrors = notificationServiceOptions.EnableDetailedErrorsSignalR);

        return services;
    }
}