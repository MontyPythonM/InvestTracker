namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Seeders;

public interface IInflationRateSeeder
{
    public Task<string> SeedAsync(bool overrideCurrentValues = false, CancellationToken token = default);
}