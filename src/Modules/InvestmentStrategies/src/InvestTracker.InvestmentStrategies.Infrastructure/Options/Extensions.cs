using InvestTracker.Shared.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Options;

internal static class Extensions
{
    public static IServiceCollection AddAppSettingsOptions(this IServiceCollection services)
    {
        var exchangeRateApiOptions = services
            .GetOptions<ExchangeRateApiOptions>("InvestmentStrategies:ExchangeRate:ExternalApiClient");
            
        var inflationRateApiOptions = services
            .GetOptions<InflationRateApiOptions>("InvestmentStrategies:InflationRate:ExternalApiClient");

        var inflationRateSeederOptions = services
            .GetOptions<InflationRateSeederOptions>("InvestmentStrategies:InflationRate:Seeder");
        
        services.AddSingleton(exchangeRateApiOptions);
        services.AddSingleton(inflationRateApiOptions);
        services.AddSingleton(inflationRateSeederOptions);

        return services;
    }
}