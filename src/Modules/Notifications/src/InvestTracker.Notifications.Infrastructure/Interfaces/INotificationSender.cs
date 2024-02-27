using InvestTracker.Notifications.Infrastructure.Dto;

namespace InvestTracker.Notifications.Infrastructure.Interfaces;

internal interface INotificationSender
{
    ValueTask SendAsync(Notification notification, CancellationToken token = default);
}