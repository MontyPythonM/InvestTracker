using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;

public interface IExchangeRateRepository
{
    Task<IEnumerable<ExchangeRate>> GetAsync(Currency fromCurrency, Currency toCurrency, DateRange dateRange, CancellationToken token = default);
}