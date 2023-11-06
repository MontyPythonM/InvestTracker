using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;

internal sealed class InvalidInflationRateException : InvestTrackerException
{
    public InvalidInflationRateException(decimal inflationRate) : base($"Inflation rate {inflationRate} is out of range.")
    {
    }
    
    public InvalidInflationRateException() : base($"Inflation rate date is out of range.")
    {
    }
}