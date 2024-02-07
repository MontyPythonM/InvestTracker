using InvestTracker.Notifications.Core.Dto;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class UserSubscriptionChangedHandler : IEventHandler<UserSubscriptionChanged>
{    
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationPublisher _notificationPublisher;
    
    public UserSubscriptionChangedHandler(IReceiverRepository receiverRepository, INotificationPublisher notificationPublisher)
    {
        _receiverRepository = receiverRepository;
        _notificationPublisher = notificationPublisher;
    }

    public async Task HandleAsync(UserSubscriptionChanged @event)
    {
        var user = await _receiverRepository.GetAsync(@event.Id);
        if (user is null)
        {
            return;
        }
        
        user.Subscription = @event.Subscription;
        
        var notification = new PersonalNotification(
            $"Your subscription was changed to '{@event.Subscription}'", 
            @event.Id);
        
        await _receiverRepository.UpdateAsync(user);
        await _notificationPublisher.NotifyAsync(notification);
    }
}