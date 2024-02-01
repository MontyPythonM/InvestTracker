using InvestTracker.Notifications.Core.Enums;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface INotificationPublisher
{
    Task PublishAsync(string message, IEnumerable<Guid> recipientIds, CancellationToken token = default);
    Task PublishAsync(string message, Guid recipientId, CancellationToken token = default);
    Task PublishAsync(string message, RecipientGroup recipientGroup, CancellationToken token = default);
    Task PublishAsync(string message, RecipientGroup recipientGroup, IEnumerable<Guid> excludedRecipientIds, CancellationToken token = default);
}