using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Entities;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Clients;

internal interface IInflationRateApiClient
{
    /// <summary>
    /// Returns null if inflation rate not found
    /// </summary>
    internal Task<InflationRateEntity?> GetInflationRateAsync(MonthlyDate monthlyDate, CancellationToken token = default);
}