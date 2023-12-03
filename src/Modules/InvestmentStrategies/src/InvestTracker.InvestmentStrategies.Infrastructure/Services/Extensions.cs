using InvestTracker.InvestmentStrategies.Infrastructure.Services.Charts;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Services;

internal static class Extensions
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IChartService, ChartService>();
        return services;
    }
}