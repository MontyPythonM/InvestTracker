using System.Linq.Expressions;
using InvestTracker.Notifications.Core.Entities.Base;
using InvestTracker.Notifications.Core.Enums;

namespace InvestTracker.Notifications.Core.Dto.Emails;

public sealed record GroupEmailMessage(RecipientGroup RecipientGroup, string Subject, string Body, 
    Expression<Func<NotificationSettings, bool>>? FilterBySetting = null, IEnumerable<Guid>? ExcludedReceiverIds = null);