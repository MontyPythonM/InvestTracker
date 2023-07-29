using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

internal class InvalidCurrencyException : InvestTrackerException
{
    public InvalidCurrencyException(string currency) : base($"Currency: '{currency}' is invalid.")
    {
    }
}