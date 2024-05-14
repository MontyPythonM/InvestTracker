namespace InvestTracker.Shared.Abstractions.Exceptions;

public sealed class DatabaseConnectionException : InvestTrackerException
{
    public DatabaseConnectionException(string message) : base(message)
    {
    }
}