using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class UserRoleGrantedHandler(
    IReceiverRepository receiverRepository, 
    INotificationPublisher notificationPublisher)
    : IEventHandler<UserRoleGranted>
{
    public async Task HandleAsync(UserRoleGranted @event)
    {
        var user = await receiverRepository.GetAsync(@event.Id, true);
        var modifiedBy = await receiverRepository.GetAsync(@event.ModifiedBy, true);

        if (user is null)
        {
            return;
        }

        user.Role = @event.Role;
        await receiverRepository.UpdateAsync(user);

        var userNotification = new PersonalNotification(
            $"Your role was set to '{@event.Role}'", 
            @event.Id);
        
        var administratorsNotification = new GroupNotification(
            $"{user.FullName.Value} role was set to {@event.Role} by {modifiedBy?.FullName.Value ?? "-"}", 
            RecipientGroup.SystemAdministrators,
            r => r.AdministratorsActivity,
            new List<Guid> { user.Id });
        
        await notificationPublisher.NotifyAsync(userNotification);
        await notificationPublisher.NotifyAsync(administratorsNotification);
    }
}