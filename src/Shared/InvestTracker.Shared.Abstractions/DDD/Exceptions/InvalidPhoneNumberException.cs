using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.Exceptions;

internal sealed class InvalidPhoneNumberException : InvestTrackerException
{
    public InvalidPhoneNumberException(string phoneNumber) : base($"Phone number '{phoneNumber}' has invalid format.")
    {
    }
}