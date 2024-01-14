using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;

public interface IInflationRateRepository
{
    public Task<IEnumerable<InflationRate>> GetInflationRatesAsync(CancellationToken token = default);
    public Task<IEnumerable<InflationRate>> GetInflationRatesAsync(DateRange dateRange, CancellationToken token = default);
    public Task<ChronologicalInflationRates> GetChronologicalInflationRatesAsync(DateRange dateRange, CancellationToken token = default);
}