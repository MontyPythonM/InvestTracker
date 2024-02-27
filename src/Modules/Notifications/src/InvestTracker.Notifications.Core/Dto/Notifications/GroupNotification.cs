using System.Linq.Expressions;
using InvestTracker.Notifications.Core.Entities.Base;
using InvestTracker.Notifications.Core.Enums;

namespace InvestTracker.Notifications.Core.Dto.Notifications;

public sealed record GroupNotification(string Message, RecipientGroup RecipientGroup, 
    Expression<Func<NotificationSettings, bool>>? FilterBySetting = null, IEnumerable<Guid>? ExcludedReceiverIds = null);