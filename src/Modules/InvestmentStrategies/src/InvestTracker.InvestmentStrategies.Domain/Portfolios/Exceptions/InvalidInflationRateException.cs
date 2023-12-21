using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class InvalidInflationRateException : InvestTrackerException
{
    public InvalidInflationRateException(decimal inflationRate) 
        : base($"Inflation rate value '{inflationRate}' is out of range.")
    {
    }
}