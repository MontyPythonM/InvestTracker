using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Authorization;

internal static class Extensions
{
    public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();
        return services;
    }
}