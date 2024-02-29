using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class RefreshTokenException : InvestTrackerException
{
    public RefreshTokenException(string message) : base(message)
    {
    }
}