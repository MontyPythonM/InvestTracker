using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.Exceptions;

internal sealed class InvalidDescriptionException : InvestTrackerException
{
    public InvalidDescriptionException() : base("Description has invalid format.")
    {
    }
}