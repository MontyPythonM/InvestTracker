using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class InvalidMarginException : InvestTrackerException
{
    public InvalidMarginException(decimal margin) : base($"Margin value: '{margin}' is invalid.")
    {
    }
}