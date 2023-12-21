using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.BackgroundJobs;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.BackgroundJobs;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Clients;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Seeders;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors;

internal static class Extensions
{
    internal static IServiceCollection AddDataCollectors(this IServiceCollection services)
    {
        services
            .AddHostedService<UpdateWithNewExchangeRatesJob>()
            .AddHostedService<UpdateWithNewInflationRatesJob>()
            .AddScoped<IInflationRateSeeder, GusPlnInflationRateSeeder>();
            
        services.AddHttpClient<IExchangeRateApiClient, NbpExchangeRateApiClient>(client =>
        {
            client.BaseAddress = new Uri(@"https://api.nbp.pl/api/exchangerates/");
            client.Timeout = new TimeSpan(0, 0, 0, 20);
        });

        services.AddHttpClient<IInflationRateApiClient, GusInflationRateApiClient>(client =>
        {
            client.BaseAddress = new Uri(@"https://api-dbw.stat.gov.pl/api/1.1.0/");
            client.Timeout = new TimeSpan(0, 0, 0, 20);
        });

        return services;
    }
}