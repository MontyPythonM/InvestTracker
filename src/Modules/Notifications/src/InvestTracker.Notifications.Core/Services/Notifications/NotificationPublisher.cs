using InvestTracker.Notifications.Core.Dto;
using InvestTracker.Notifications.Core.Enums;
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

    public async Task PublishAsync(string message, IEnumerable<Guid> recipientIds, CancellationToken token = default)
    {
        var recipients = (await _receiverRepository.GetAsync(recipientIds, true, token))
            .Where(r => r.PersonalSettings.EnableNotifications)
            .Select(r => r.Id)
            .ToHashSet();
        
        await _notificationSender.SendAsync(new Notification(message, recipients), token);
    }

    public async Task PublishAsync(string message, Guid recipientId, CancellationToken token = default)
    { 
        await PublishAsync(message, new List<Guid> { recipientId }, token);
    }

    public async Task PublishAsync(string message, RecipientGroup recipientGroup, CancellationToken token = default)
    {
        var recipients = (await _receiverRepository.GetAsync(recipientGroup, true, token))
            .Where(r => r.PersonalSettings.EnableNotifications)
            .Select(r => r.Id)
            .ToHashSet();
        
        await _notificationSender.SendAsync(new Notification(message, recipients), token);
    }

    public async Task PublishAsync(string message, RecipientGroup recipientGroup, IEnumerable<Guid> excludedRecipientIds,
        CancellationToken token = default)
    {
        var recipients = (await _receiverRepository.GetAsync(recipientGroup, true, token))
            .Where(r => r.PersonalSettings.EnableNotifications)
            .Select(r => r.Id)
            .Except(excludedRecipientIds)
            .ToHashSet();
        
        await _notificationSender.SendAsync(new Notification(message, recipients), token);
    }
}