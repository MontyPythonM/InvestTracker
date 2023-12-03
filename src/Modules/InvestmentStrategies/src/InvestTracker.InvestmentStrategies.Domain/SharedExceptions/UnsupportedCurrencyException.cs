using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.SharedExceptions;

public sealed class UnsupportedCurrencyException : InvestTrackerException
{
    public UnsupportedCurrencyException(string currency) : base($"Currency {currency} is unsupported.")
    {
    }
}