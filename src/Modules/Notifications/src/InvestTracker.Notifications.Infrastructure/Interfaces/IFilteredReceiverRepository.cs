using System.Linq.Expressions;
using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Entities.Base;
using InvestTracker.Notifications.Core.Enums;

namespace InvestTracker.Notifications.Infrastructure.Interfaces;

internal interface IFilteredReceiverRepository
{
    Task<Receiver?> GetFilteredAsync(Guid id, Expression<Func<NotificationSettings, bool>>? filterBy = null, bool asNoTracking = false, CancellationToken token = default);
    Task<IEnumerable<Receiver>> GetFilteredAsync(IEnumerable<Guid> receiversIds, Expression<Func<NotificationSettings, bool>>? filterBy = null, bool asNoTracking = false, CancellationToken token = default);
    Task<IEnumerable<Receiver>> GetFilteredAsync(RecipientGroup recipientGroup, Expression<Func<NotificationSettings, bool>>? filterBy = null, bool asNoTracking = false, CancellationToken token = default);
}