using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class InvalidAmountException : InvestTrackerException
{
    public InvalidAmountException(decimal amount) : base($"Amount: '{amount}' is invalid.")
    {
    }
}