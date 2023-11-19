using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class InvalidInflationRatesCurrencyException : InvestTrackerException
{
    public InvalidInflationRatesCurrencyException() : base($"Inflation rates are not for the same currency")
    {
    }
}