using InvestTracker.Notifications.Core.Dto;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface INotificationPublisher
{
    ValueTask PublishAsync(Notification notification);
}