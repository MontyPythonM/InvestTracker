using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class UserRoleGrantedHandler : IEventHandler<UserRoleGranted>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationPublisher _notificationPublisher;

    public UserRoleGrantedHandler(IReceiverRepository receiverRepository, INotificationPublisher notificationPublisher)
    {
        _receiverRepository = receiverRepository;
        _notificationPublisher = notificationPublisher;
    }

    public async Task HandleAsync(UserRoleGranted @event)
    {
        var user = await _receiverRepository.GetAsync(@event.Id, true);
        var modifiedBy = await _receiverRepository.GetAsync(@event.ModifiedBy, true);

        if (user is null)
        {
            return;
        }

        user.Role = @event.Role;
        await _receiverRepository.UpdateAsync(user);

        var userNotification = new PersonalNotification(
            $"Your role was set to '{@event.Role}'", 
            @event.Id);
        
        var administratorsNotification = new GroupNotification(
            $"{user.FullName.Value} role was set to {@event.Role} by {modifiedBy?.FullName.Value ?? "-"}", 
            RecipientGroup.SystemAdministrators,
            r => r.AdministratorsActivity,
            new List<Guid> { user.Id });
        
        await _notificationPublisher.NotifyAsync(userNotification);
        await _notificationPublisher.NotifyAsync(administratorsNotification);
    }
}