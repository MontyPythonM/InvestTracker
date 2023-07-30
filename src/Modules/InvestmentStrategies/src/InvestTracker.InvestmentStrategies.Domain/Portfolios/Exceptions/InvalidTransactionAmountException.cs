using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class InvalidTransactionAmountException : InvestTrackerException
{
    public InvalidTransactionAmountException(decimal amount) : base($"Transfer has invalid amount: '{amount}'.")
    {
    }
}