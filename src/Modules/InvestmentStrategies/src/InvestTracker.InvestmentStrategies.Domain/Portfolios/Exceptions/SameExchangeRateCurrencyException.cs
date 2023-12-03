using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class SameExchangeRateCurrencyException : InvestTrackerException
{
    public SameExchangeRateCurrencyException() : base($"Exchange rate must have different two currencies")
    {
    }
}