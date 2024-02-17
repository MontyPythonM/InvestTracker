using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Notifications.Core.Exceptions;

internal sealed class EmailSenderException : InvestTrackerException
{
    public EmailSenderException(string message) : base(message)
    {
    }
}