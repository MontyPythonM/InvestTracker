using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Entities;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients;

public interface IExchangeRateApiClient
{
    Task<IEnumerable<ExchangeRateEntity>> GetConcreteCurrencyAsync(Currency currency, DateRange dateRange, CancellationToken token = default);
    Task<IEnumerable<ExchangeRateEntity>> GetAllAsync(DateRange dateRange, CancellationToken token = default);
}