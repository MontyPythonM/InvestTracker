using InvestTracker.Notifications.Core.Dto.Notifications;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface INotificationSender
{
    ValueTask SendAsync(Notification notification, CancellationToken token = default);
}