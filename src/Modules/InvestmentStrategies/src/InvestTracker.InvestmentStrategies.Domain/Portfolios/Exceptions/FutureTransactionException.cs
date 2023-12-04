using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class FutureTransactionException : InvestTrackerException
{
    public FutureTransactionException() : base($"Transaction cannot have a future date")
    {
    }
}