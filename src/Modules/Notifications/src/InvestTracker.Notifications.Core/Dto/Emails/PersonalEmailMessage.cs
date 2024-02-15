using System.Linq.Expressions;
using InvestTracker.Notifications.Core.Entities;

namespace InvestTracker.Notifications.Core.Dto.Emails;

public sealed record PersonalEmailMessage
{
    public ISet<Guid> Recipients { get; }
    public string Subject { get; set; }
    public string Body { get; }
    public Expression<Func<Receiver, bool>>? FilterBySetting { get; }
    
    public PersonalEmailMessage(IEnumerable<Guid> recipients, string subject, string body, Expression<Func<Receiver, bool>>? filterBySetting = null)
    {
        Body = body;
        Subject = subject;
        Recipients = recipients.ToHashSet();
        FilterBySetting = filterBySetting;
    }

    public PersonalEmailMessage(Guid recipient, string subject, string body, Expression<Func<Receiver, bool>>? filterBySetting = null)
    {
        Body = body;
        Subject = subject;
        Recipients = new HashSet<Guid> { recipient };
        FilterBySetting = filterBySetting;
    }
}