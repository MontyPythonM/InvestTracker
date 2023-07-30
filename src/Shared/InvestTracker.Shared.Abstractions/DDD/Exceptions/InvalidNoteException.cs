using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.Exceptions;

internal sealed class InvalidNoteException : InvestTrackerException
{
    public InvalidNoteException() : base($"Note has invalid format.")
    {
    }
}