using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.BackgroundTasks;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.BackgroundTasks;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Clients;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Seeders;
using InvestTracker.InvestmentStrategies.Infrastructure.Options;
using InvestTracker.Shared.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors;

internal static class Extensions
{
    internal static IServiceCollection AddDataCollectors(this IServiceCollection services)
    {
        services
            .AddHostedService<UpdateExchangeRatesTask>()
            .AddHostedService<UpdateInflationRatesTask>()
            .AddScoped<IInflationRateSeeder, GusPlnInflationRateSeeder>();
            
        var exchangeRateApiOptions = services
            .GetOptions<ExchangeRateApiOptions>("InvestmentStrategies:ExchangeRate:ExternalApiClient");
            
        var inflationRateApiOptions = services
            .GetOptions<InflationRateApiOptions>("InvestmentStrategies:InflationRate:ExternalApiClient");
        
        services.AddHttpClient<IExchangeRateApiClient, NbpExchangeRateApiClient>(client =>
        {
            client.BaseAddress = new Uri(exchangeRateApiOptions.BaseUrl);
            client.Timeout = new TimeSpan(0, 0, 0, exchangeRateApiOptions.TimeoutSeconds);
        });

        services.AddHttpClient<IInflationRateApiClient, GusInflationRateApiClient>(client =>
        {
            client.BaseAddress = new Uri(inflationRateApiOptions.BaseUrl);
            client.Timeout = new TimeSpan(0, 0, 0, inflationRateApiOptions.TimeoutSeconds);
        });

        return services;
    }
}