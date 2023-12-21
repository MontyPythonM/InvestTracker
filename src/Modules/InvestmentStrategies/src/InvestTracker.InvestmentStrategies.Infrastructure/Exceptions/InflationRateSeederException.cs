using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Exceptions;

internal sealed class InflationRateSeederException : InvestTrackerException
{
    public InflationRateSeederException(string message) : base(message)
    {
    }
}