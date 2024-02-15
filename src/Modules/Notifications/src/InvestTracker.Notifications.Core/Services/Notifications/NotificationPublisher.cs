using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;

namespace InvestTracker.Notifications.Core.Services.Notifications;

internal sealed class NotificationPublisher : INotificationPublisher
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationSender _notificationSender;

    public NotificationPublisher(IReceiverRepository receiverRepository, INotificationSender notificationSender)
    {
        _receiverRepository = receiverRepository;
        _notificationSender = notificationSender;
    }

    public async Task PublishAsync(PersonalNotification notification, CancellationToken token = default)
    {
        var receivers = await _receiverRepository.GetAsync(notification.Recipients, notification.FilterBySetting, true, token);
        
        var filteredRecipients = receivers
            .Where(r => r.PersonalSettings.EnableNotifications)
            .Select(r => r.Id)
            .ToHashSet();

        await _notificationSender.SendAsync(new Notification(notification.Message, filteredRecipients), token);
    }

    public async Task PublishAsync(GroupNotification notification, CancellationToken token = default)
    {
        var receivers = await _receiverRepository.GetAsync(notification.RecipientGroup, notification.FilterBySetting, true, token);

        var recipients = receivers.Where(r => r.PersonalSettings.EnableNotifications);
        
        if (notification.ExcludedReceiverIds is not null)
        {
            recipients = recipients.ExceptBy(notification.ExcludedReceiverIds, r => r.Id);
        }

        var filteredRecipients = recipients.Select(r => r.Id).ToHashSet();
        
        await _notificationSender.SendAsync(new Notification(notification.Message, filteredRecipients), token);
    }
}