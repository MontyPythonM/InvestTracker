using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;

public interface IInflationRateRepository
{
    public Task<IEnumerable<InflationRate>> GetInflationRates(CancellationToken token = default);
    public Task<IEnumerable<InflationRate>> GetInflationRates(DateRange dateRange, CancellationToken token = default);
    public Task<ChronologicalInflationRates> GetChronologicalInflationRates(DateRange dateRange, CancellationToken token = default);
}