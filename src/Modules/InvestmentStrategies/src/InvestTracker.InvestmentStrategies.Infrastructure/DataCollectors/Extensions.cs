using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.NbpTableA;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors;

internal static class Extensions
{
    internal static IServiceCollection AddDataCollectors(this IServiceCollection services)
        => services.AddScoped<IExchangeRateSeeder, NbpExchangeRateSeeder>();
}