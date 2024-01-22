using System.Runtime.CompilerServices;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Notifications.Core.Persistence;
using InvestTracker.Notifications.Core.Persistence.Repositories;
using InvestTracker.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Notifications.Api")]
namespace InvestTracker.Notifications.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services
            .AddPostgres<NotificationsDbContext>()
            .AddScoped<IReceiverRepository, ReceiverRepository>()
            .AddScoped<IGlobalNotificationSetupRepository, GlobalNotificationSetupRepository>()
            .AddSignalR();

        return services;
    }
}