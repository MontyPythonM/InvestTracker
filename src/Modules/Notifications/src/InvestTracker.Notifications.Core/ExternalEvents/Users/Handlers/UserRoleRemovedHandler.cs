using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class UserRoleRemovedHandler : IEventHandler<UserRoleRemoved>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationPublisher _notificationPublisher;
    
    public UserRoleRemovedHandler(IReceiverRepository receiverRepository, INotificationPublisher notificationPublisher)
    {
        _receiverRepository = receiverRepository;
        _notificationPublisher = notificationPublisher;
    }
    
    public async Task HandleAsync(UserRoleRemoved @event)
    {
        var user = await _receiverRepository.GetAsync(@event.Id);
        var modifiedBy = await _receiverRepository.GetAsync(@event.ModifiedBy, null, true);

        if (user is null)
        {
            return;
        }
        
        user.Role = SystemRole.None;
        await _receiverRepository.UpdateAsync(user);

        var userNotification = new PersonalNotification(
            $"Your role was set to '{SystemRole.None}'", 
            @event.Id);
        
        var administratorsNotification = new GroupNotification(
            $"{user.FullName.Value} role was set to '{SystemRole.None}' by {modifiedBy?.FullName.Value ?? "-"}", 
            RecipientGroup.SystemAdministrators,
            r => r.PersonalSettings.AdministratorsActivity,
            new List<Guid> { user.Id });
        
        await _notificationPublisher.PublishAsync(userNotification);
        await _notificationPublisher.PublishAsync(administratorsNotification);
    }
}