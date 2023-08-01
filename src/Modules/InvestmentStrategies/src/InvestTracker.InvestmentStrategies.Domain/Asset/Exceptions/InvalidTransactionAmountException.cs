using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

internal sealed class InvalidTransactionAmountException : InvestTrackerException
{
    public InvalidTransactionAmountException(decimal amount) : base($"Transfer has invalid amount: '{amount}'.")
    {
    }
}