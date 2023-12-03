using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Exceptions;

internal sealed class ExchangeRateNotFoundException : InvestTrackerException
{
    public ExchangeRateNotFoundException(DateRange dateRange) 
        : base($"Exchange rates for date range from {dateRange.From} to {dateRange.To} not found")
    {
    }
}