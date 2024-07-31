using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class UserRoleRemovedHandler(
    IReceiverRepository receiverRepository, 
    INotificationPublisher notificationPublisher)
    : IEventHandler<UserRoleRemoved>
{
    public async Task HandleAsync(UserRoleRemoved @event)
    {
        var user = await receiverRepository.GetAsync(@event.Id, true);
        var modifiedBy = await receiverRepository.GetAsync(@event.ModifiedBy, true);

        if (user is null)
        {
            return;
        }
        
        user.Role = SystemRole.None;
        await receiverRepository.UpdateAsync(user);

        var userNotification = new PersonalNotification(
            $"Your role was set to '{SystemRole.None}'", 
            @event.Id);
        
        var administratorsNotification = new GroupNotification(
            $"{user.FullName.Value} role was set to {SystemRole.None} by {modifiedBy?.FullName.Value ?? "-"}", 
            RecipientGroup.SystemAdministrators,
            r => r.AdministratorsActivity,
            new List<Guid> { user.Id });
        
        await notificationPublisher.NotifyAsync(userNotification);
        await notificationPublisher.NotifyAsync(administratorsNotification);
    }
}