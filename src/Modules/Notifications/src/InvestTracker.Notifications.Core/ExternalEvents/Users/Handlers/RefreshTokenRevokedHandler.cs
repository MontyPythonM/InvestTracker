using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class RefreshTokenRevokedHandler(
    IReceiverRepository receiverRepository, 
    INotificationPublisher notificationPublisher)
    : IEventHandler<RefreshTokenRevoked>
{
    public async Task HandleAsync(RefreshTokenRevoked @event)
    {
        var user = await receiverRepository.GetAsync(@event.UserId, true);

        if (user is null)
        {
            return;
        }
        
        var notification = new PersonalNotification(
            $"{user.FullName.Value} refresh token was revoked", 
            @event.ModifiedBy,
            r => r.AdministratorsActivity);
        
        await notificationPublisher.NotifyAsync(notification);
    }
}