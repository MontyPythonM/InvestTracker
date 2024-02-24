using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class UserSubscriptionChangedHandler : IEventHandler<UserSubscriptionChanged>
{    
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationPublisher _notificationPublisher;
    private readonly IEmailPublisher _emailPublisher;
    
    public UserSubscriptionChangedHandler(IReceiverRepository receiverRepository, 
        INotificationPublisher notificationPublisher, IEmailPublisher emailPublisher)
    {
        _receiverRepository = receiverRepository;
        _notificationPublisher = notificationPublisher;
        _emailPublisher = emailPublisher;
    }

    public async Task HandleAsync(UserSubscriptionChanged @event)
    {
        var user = await _receiverRepository.GetAsync(@event.Id);
        if (user is null)
        {
            return;
        }
        
        user.Subscription = @event.Subscription;
        
        var message = $"Your subscription was changed to '{@event.Subscription}'";
        var notification = new PersonalNotification(message, @event.Id);
        var email = new PersonalEmailMessage(@event.Id, "Subscription changed", message);
        
        await _receiverRepository.UpdateAsync(user);
        await _notificationPublisher.NotifyAsync(notification);
        await _emailPublisher.NotifyAsync(email);
    }
}