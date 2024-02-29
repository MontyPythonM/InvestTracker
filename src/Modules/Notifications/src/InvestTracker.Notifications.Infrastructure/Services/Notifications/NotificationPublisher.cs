using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Notifications.Infrastructure.Dto;
using InvestTracker.Notifications.Infrastructure.Interfaces;

namespace InvestTracker.Notifications.Infrastructure.Services.Notifications;

internal sealed class NotificationPublisher : INotificationPublisher
{
    private readonly IFilteredReceiverRepository _filteredReceiverRepository;
    private readonly INotificationSender _notificationSender;

    public NotificationPublisher(IFilteredReceiverRepository filteredReceiverRepository, INotificationSender notificationSender)
    {
        _filteredReceiverRepository = filteredReceiverRepository;
        _notificationSender = notificationSender;
    }

    public async Task NotifyAsync(PersonalNotification notification, CancellationToken token = default)
    {
        var receivers = await _filteredReceiverRepository.GetFilteredAsync(notification.Recipients, notification.FilterBySetting, true, token);
        
        var filteredRecipients = receivers
            .Where(r => r.PersonalSettings.EnableNotifications)
            .Select(r => r.Id)
            .ToHashSet();

        await _notificationSender.SendAsync(new Notification(notification.Message, filteredRecipients), token);
    }

    public async Task NotifyAsync(GroupNotification notification, CancellationToken token = default)
    {
        var receivers = await _filteredReceiverRepository.GetFilteredAsync(notification.RecipientGroup, notification.FilterBySetting, true, token);
        var recipients = receivers.Where(r => r.PersonalSettings.EnableNotifications);
        
        if (notification.ExcludedReceiverIds is not null)
        {
            recipients = recipients.ExceptBy(notification.ExcludedReceiverIds, r => r.Id);
        }

        var filteredRecipients = recipients.Select(r => r.Id).ToHashSet();
        
        await _notificationSender.SendAsync(new Notification(notification.Message, filteredRecipients), token);
    }

    public async Task NotifyAsync(DirectNotification notification, CancellationToken token = default)
    {
        await _notificationSender.SendAsync(new Notification(notification.Message, notification.Recipients), token);
    }
}