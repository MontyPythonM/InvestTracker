using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Enums;
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
        var modifiedBy = await receiverRepository.GetAsync(@event.ModifiedBy, true);

        if (user is null)
        {
            return;
        }
        
        var administratorsNotification = new GroupNotification(
            $"{user.FullName.Value} refresh token was revoked by {modifiedBy?.FullName.Value ?? "-"}", 
            RecipientGroup.SystemAdministrators,
            r => r.AdministratorsActivity);
        
        await notificationPublisher.NotifyAsync(administratorsNotification);
    }
}