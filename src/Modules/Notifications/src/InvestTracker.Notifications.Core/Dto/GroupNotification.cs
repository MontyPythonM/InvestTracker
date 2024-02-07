using System.Linq.Expressions;
using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Enums;

namespace InvestTracker.Notifications.Core.Dto;

public sealed record GroupNotification(string Message, RecipientGroup RecipientGroup, 
    Expression<Func<Receiver, bool>>? FilterBySetting = null, IEnumerable<Guid>? ExcludedReceiverIds = null);