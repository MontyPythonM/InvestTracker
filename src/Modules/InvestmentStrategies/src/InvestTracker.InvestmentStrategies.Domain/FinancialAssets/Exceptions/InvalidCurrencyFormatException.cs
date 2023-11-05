using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;

internal sealed class InvalidCurrencyFormatException : InvestTrackerException
{
    public InvalidCurrencyFormatException(string currency) : base($"Currency {currency} has invalid format.")
    {
    }
}