using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

internal class InvalidCurrencyFormatException : InvestTrackerException
{
    public InvalidCurrencyFormatException(string currency) : base($"Currency {currency} has invalid format.")
    {
    }
}