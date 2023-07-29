using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects.Exceptions;

public sealed class InvalidTitleException : InvestTrackerException
{
    public InvalidTitleException(string title) : base($"Title '{title}' has invalid format.")
    {
    }
}