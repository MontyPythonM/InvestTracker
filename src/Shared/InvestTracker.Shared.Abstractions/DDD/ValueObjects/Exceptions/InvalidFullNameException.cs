using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects.Exceptions;

public sealed class InvalidFullNameException : InvestTrackerException
{
    public InvalidFullNameException(string fullName) : base($"Fullname '{fullName}' has invalid format.")
    {
    }
}