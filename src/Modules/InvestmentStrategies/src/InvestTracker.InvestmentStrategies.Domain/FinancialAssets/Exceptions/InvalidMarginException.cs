using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;

internal sealed class InvalidMarginException : InvestTrackerException
{
    public InvalidMarginException(decimal margin) : base($"Margin value: '{margin}' is invalid.")
    {
    }
}