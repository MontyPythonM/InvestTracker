using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal class InflationRateNotFoundException : InvestTrackerException
{
    public InflationRateNotFoundException(MonthlyDate monthlyDate) : base($"Inflation rate for date: '{monthlyDate}' not found")
    {
    }
}