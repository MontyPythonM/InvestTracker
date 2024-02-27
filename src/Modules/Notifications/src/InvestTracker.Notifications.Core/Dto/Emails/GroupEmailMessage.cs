using System.Linq.Expressions;
using InvestTracker.Notifications.Core.Entities.Base;
using InvestTracker.Notifications.Core.Enums;

namespace InvestTracker.Notifications.Core.Dto.Emails;

public sealed record GroupEmailMessage
{
    public RecipientGroup RecipientGroup { get; }
    public string Subject { get; }
    public string Body { get; }
    public string HtmlBody { get; }
    public Expression<Func<NotificationSettings, bool>>? FilterBySetting { get; } 
    public IEnumerable<Guid>? ExcludedReceiverIds { get; }

    public GroupEmailMessage(RecipientGroup recipientGroup, string subject, string body, 
        Expression<Func<NotificationSettings, bool>>? filterBySetting = null, IEnumerable<Guid>? excludedReceiverIds = null)
    {
        RecipientGroup = recipientGroup;
        Subject = subject;
        Body = body;
        FilterBySetting = filterBySetting;
        ExcludedReceiverIds = excludedReceiverIds;
    }

    public GroupEmailMessage(RecipientGroup recipientGroup, string subject, string body, string htmlBody, 
        Expression<Func<NotificationSettings, bool>>? filterBySetting = null, IEnumerable<Guid>? excludedReceiverIds = null)
    {
        RecipientGroup = recipientGroup;
        Subject = subject;
        Body = body;
        HtmlBody = htmlBody;
        FilterBySetting = filterBySetting;
        ExcludedReceiverIds = excludedReceiverIds;
    }
}