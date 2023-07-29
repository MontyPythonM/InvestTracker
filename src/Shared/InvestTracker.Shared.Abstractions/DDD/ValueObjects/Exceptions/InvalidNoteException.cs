using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects.Exceptions;

internal sealed class InvalidNoteException : InvestTrackerException
{
    public InvalidNoteException() : base($"Note has invalid format.")
    {
    }
}