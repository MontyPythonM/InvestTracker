using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

internal sealed class InvalidNoteException : InvestTrackerException
{
    public InvalidNoteException() : base($"Note has invalid format.")
    {
    }
}