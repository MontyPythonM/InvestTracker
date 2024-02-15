using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.Notifications.Core.Dto.Emails;

public sealed record EmailMessage
{
    public ISet<Email> Recipients { get; }
    public string Subject { get; }
    public string Body { get; }

    public EmailMessage(ISet<Email> recipients, string subject, string body)
    {
        Recipients = recipients;
        Subject = subject;
        Body = body;
    }
    
    public EmailMessage(Email recipient, string subject, string body)
    {
        Recipients = new HashSet<Email> { recipient };
        Subject = subject;
        Body = body;
    }
}