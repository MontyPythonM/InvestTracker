namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates;

public interface IExchangeRateSeeder
{
    Task SeedAsync(bool forceSeed = false, CancellationToken token = default);
}