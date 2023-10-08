using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Exceptions;

internal sealed class CannotConnectToDatabaseException : InvestTrackerException
{
    public CannotConnectToDatabaseException() : base("Cannot connect to database")
    {
    }
}