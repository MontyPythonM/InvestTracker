using System.Linq.Expressions;
using InvestTracker.Notifications.Core.Entities.Base;

namespace InvestTracker.Notifications.Core.Dto.Emails;

public sealed record PersonalEmailMessage
{
    public ISet<Guid> Recipients { get; }
    public string Subject { get; set; }
    public string Body { get; }
    public string HtmlBody { get; }
    public Expression<Func<NotificationSettings, bool>>? FilterBySetting { get; }
    
    public PersonalEmailMessage(IEnumerable<Guid> recipients, string subject, string body, 
        Expression<Func<NotificationSettings, bool>>? filterBySetting = null)
    {
        Body = body;
        Subject = subject;
        Recipients = recipients.ToHashSet();
        FilterBySetting = filterBySetting;
    }

    public PersonalEmailMessage(Guid recipient, string subject, string body, 
        Expression<Func<NotificationSettings, bool>>? filterBySetting = null)
    {
        Body = body;
        Subject = subject;
        Recipients = new HashSet<Guid> { recipient };
        FilterBySetting = filterBySetting;
    }
    
    public PersonalEmailMessage(IEnumerable<Guid> recipients, string subject, string body, string htmlBody, 
        Expression<Func<NotificationSettings, bool>>? filterBySetting = null)
    {
        Body = body;
        Subject = subject;
        Recipients = recipients.ToHashSet();
        FilterBySetting = filterBySetting;
        HtmlBody = htmlBody;
    }

    public PersonalEmailMessage(Guid recipient, string subject, string body, string htmlBody, 
        Expression<Func<NotificationSettings, bool>>? filterBySetting = null)
    {
        Body = body;
        Subject = subject;
        Recipients = new HashSet<Guid> { recipient };
        FilterBySetting = filterBySetting;
        HtmlBody = htmlBody;
    }
}