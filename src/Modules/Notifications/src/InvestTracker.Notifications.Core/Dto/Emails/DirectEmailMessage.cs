using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.Notifications.Core.Dto.Emails;

public sealed record DirectEmailMessage
{
    public ISet<Email> Recipients { get; }
    public string Subject { get; }
    public string Body { get; }
    public string HtmlBody { get; }

    public DirectEmailMessage(ISet<Email> recipients, string subject, string body)
    {
        Recipients = recipients;
        Subject = subject;
        Body = body;
    }
    
    public DirectEmailMessage(Email recipient, string subject, string body)
    {
        Recipients = new HashSet<Email> { recipient };
        Subject = subject;
        Body = body;
    }
    
    public DirectEmailMessage(ISet<Email> recipients, string subject, string body, string htmlBody)
    {
        Recipients = recipients;
        Subject = subject;
        Body = body;
        HtmlBody = htmlBody;
    }
    
    public DirectEmailMessage(Email recipient, string subject, string body, string htmlBody)
    {
        Recipients = new HashSet<Email> { recipient };
        Subject = subject;
        Body = body;
        HtmlBody = htmlBody;
    }
}