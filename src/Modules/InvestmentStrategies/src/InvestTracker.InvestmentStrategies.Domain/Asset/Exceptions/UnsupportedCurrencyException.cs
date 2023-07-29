using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

internal class UnsupportedCurrencyException : InvestTrackerException
{
    public UnsupportedCurrencyException(string currency) : base($"Currency: {currency} is not supported.")
    {
    }
}