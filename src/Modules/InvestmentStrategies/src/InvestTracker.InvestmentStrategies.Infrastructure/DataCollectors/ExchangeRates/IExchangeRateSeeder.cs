namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates;

public interface IExchangeRateSeeder
{
    Task SeedAsync(CancellationToken token = default);
}