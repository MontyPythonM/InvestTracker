namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates;

public interface IExchangeRateSeeder
{
    Task SeedAsync(bool seedIfTableIsEmpty = true, CancellationToken token = default);
}