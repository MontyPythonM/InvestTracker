using InvestTracker.Notifications.Core.Dto;
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

    public async Task NotifyAsync(PersonalNotification notification, CancellationToken token = default)
    {
        var recipients = await _receiverRepository.GetAsync(notification.Recipients, notification.FilterBySetting, true, token);
        
        var filteredRecipients = recipients
            .Where(r => r.PersonalSettings.EnableNotifications)
            .Select(r => r.Id)
            .ToHashSet();

        await _notificationSender.SendAsync(new Notification(notification.Message, filteredRecipients), token);
    }

    public async Task NotifyAsync(GroupNotification notification, CancellationToken token = default)
    {
        var recipients = await _receiverRepository.GetAsync(notification.RecipientGroup, notification.FilterBySetting, true, token);

        var query = recipients
            .Where(r => r.PersonalSettings.EnableNotifications)
            .AsQueryable();
        
        if (notification.ExcludedReceiverIds is not null)
        {
            query = query.ExceptBy(notification.ExcludedReceiverIds, x => x.Id);
        }

        var filteredRecipients = query.Select(r => r.Id).ToHashSet();
        
        await _notificationSender.SendAsync(new Notification(notification.Message, filteredRecipients), token);
    }
}