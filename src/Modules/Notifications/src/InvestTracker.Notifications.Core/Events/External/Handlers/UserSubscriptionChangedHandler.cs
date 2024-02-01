using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

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
        
        await _receiverRepository.UpdateAsync(user);
        await _notificationPublisher.PublishAsync($"Your subscription was changed to '{@event.Subscription}'", @event.Id);
    }
}