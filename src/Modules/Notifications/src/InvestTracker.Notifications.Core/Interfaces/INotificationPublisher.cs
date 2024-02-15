using InvestTracker.Notifications.Core.Dto.Notifications;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface INotificationPublisher
{
    Task PublishAsync(PersonalNotification notification, CancellationToken token = default);
    Task PublishAsync(GroupNotification notification, CancellationToken token = default);
}