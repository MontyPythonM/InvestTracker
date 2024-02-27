using System.Linq.Expressions;
using InvestTracker.Notifications.Core.Entities.Base;

namespace InvestTracker.Notifications.Core.Dto.Notifications;

public sealed record PersonalNotification
{
    public string Message { get; }
    public ISet<Guid> Recipients { get; }
    public Expression<Func<NotificationSettings, bool>>? FilterBySetting { get; }
    
    public PersonalNotification(string message, IEnumerable<Guid> recipients, Expression<Func<NotificationSettings, bool>>? filterBySetting = null)
    {
        Message = message;
        Recipients = recipients.ToHashSet();
        FilterBySetting = filterBySetting;
    }

    public PersonalNotification(string message, Guid recipient, Expression<Func<NotificationSettings, bool>>? filterBySetting = null)
    {
        Message = message;
        Recipients = new HashSet<Guid> { recipient };
        FilterBySetting = filterBySetting;
    }
}