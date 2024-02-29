using InvestTracker.Notifications.Core.Dto.Notifications;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface INotificationPublisher
{
    Task NotifyAsync(PersonalNotification notification, CancellationToken token = default);
    Task NotifyAsync(GroupNotification notification, CancellationToken token = default);
    Task NotifyAsync(DirectNotification notification, CancellationToken token = default);
}