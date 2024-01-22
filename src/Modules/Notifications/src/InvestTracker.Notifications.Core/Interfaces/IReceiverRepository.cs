using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface IReceiverRepository
{
    Task<Receiver?> GetAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<Receiver>> GetAsync(Role role, CancellationToken token = default);
    Task<IEnumerable<Receiver>> GetAsync(Subscription subscription, CancellationToken token = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken token = default);
    Task CreateAsync(Receiver receiver, CancellationToken token = default);
    Task UpdateAsync(Receiver receiver, CancellationToken token = default);
}