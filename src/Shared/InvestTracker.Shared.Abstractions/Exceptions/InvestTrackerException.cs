namespace InvestTracker.Shared.Abstractions.Exceptions;

/// <summary>
/// Abstract base exception
/// </summary>
public abstract class InvestTrackerException : Exception
{
    protected InvestTrackerException(string message) : base(message)
    {
    }
}