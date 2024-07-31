using InvestTracker.Notifications.Core.Dto.Emails;
using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class UserSubscriptionChangedHandler(
    IReceiverRepository receiverRepository,
    INotificationPublisher notificationPublisher,
    IEmailPublisher emailPublisher)
    : IEventHandler<UserSubscriptionChanged>
{
    public async Task HandleAsync(UserSubscriptionChanged @event)
    {
        var user = await receiverRepository.GetAsync(@event.Id);
        if (user is null)
        {
            return;
        }
        
        user.Subscription = @event.Subscription;
        await receiverRepository.UpdateAsync(user);

        await NotifyUserAsync(@event);
        await NotifyAdminsAsync(@event);
    }
    
    private async Task NotifyUserAsync(UserSubscriptionChanged @event)
    {
        var message = $"Your subscription was changed to {@event.Subscription}";
        var notification = new PersonalNotification(message, @event.Id);
        var email = new PersonalEmailMessage(@event.Id, "Subscription changed", message);
        
        await notificationPublisher.NotifyAsync(notification);
        await emailPublisher.NotifyAsync(email);
    }
    
    private async Task NotifyAdminsAsync(UserSubscriptionChanged @event)
    {
        var notification = new PersonalNotification(
            $"{@event.FullName} subscription changed on {@event.Subscription}", 
            @event.ModifiedBy,
            setting => setting.AdministratorsActivity);
        
        await notificationPublisher.NotifyAsync(notification);
    }
}