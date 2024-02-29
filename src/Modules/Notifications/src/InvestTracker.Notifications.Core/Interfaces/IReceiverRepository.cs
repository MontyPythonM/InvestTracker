using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Enums;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface IReceiverRepository
{
    Task<Receiver?> GetAsync(Guid id, bool asNoTracking = false, CancellationToken token = default);
    Task<IEnumerable<Receiver>> GetAsync(RecipientGroup recipientGroup, bool asNoTracking = false, CancellationToken token = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken token = default);
    Task CreateAsync(Receiver receiver, CancellationToken token = default);
    Task UpdateAsync(Receiver receiver, CancellationToken token = default);
    Task DeleteAsync(Receiver receiver, CancellationToken token = default);
}