using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.BackgroundJobs;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors;

internal static class Extensions
{
    internal static IServiceCollection AddDataCollectors(this IServiceCollection services)
    {
        services
            .AddHostedService<UpdateWithNewExchangeRatesJob>();
            
        services.AddHttpClient<IExchangeRateApiClient, NbpExchangeRateApiClient>(client =>
        {
            client.BaseAddress = new Uri(@"https://api.nbp.pl/api/exchangerates/");
            client.Timeout = new TimeSpan(0, 0, 1, 30);
        });

        return services;
    }
}