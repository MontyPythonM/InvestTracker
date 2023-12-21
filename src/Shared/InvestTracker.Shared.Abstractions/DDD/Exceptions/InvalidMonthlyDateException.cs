using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.Exceptions;

public sealed class InvalidMonthlyDateException : InvestTrackerException
{
    public InvalidMonthlyDateException(string message) : base(message)
    {
    }
}