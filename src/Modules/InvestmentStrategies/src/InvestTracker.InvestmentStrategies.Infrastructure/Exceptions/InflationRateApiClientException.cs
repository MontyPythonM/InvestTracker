using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Exceptions;

internal sealed class InflationRateApiClientException : InvestTrackerException
{
    public InflationRateApiClientException(string message) : base(message)
    {
    }
}