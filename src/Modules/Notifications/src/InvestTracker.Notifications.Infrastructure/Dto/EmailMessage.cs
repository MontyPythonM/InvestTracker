using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.Notifications.Infrastructure.Dto;

public sealed record EmailMessage
{
    public ISet<Email> Recipients { get; }
    public string Subject { get; }
    public string Body { get; }
    public string HtmlBody { get; }

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
    
    public EmailMessage(ISet<Email> recipients, string subject, string body, string htmlBody)
    {
        Recipients = recipients;
        Subject = subject;
        Body = body;
        HtmlBody = htmlBody;
    }
    
    public EmailMessage(Email recipient, string subject, string body, string htmlBody)
    {
        Recipients = new HashSet<Email> { recipient };
        Subject = subject;
        Body = body;
        HtmlBody = htmlBody;
    }
}