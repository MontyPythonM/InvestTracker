using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

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
        var user = await _receiverRepository.GetAsync(@event.Id);
        if (user is null)
        {
            return;
        }

        await _notificationPublisher.PublishAsync($"Your role was set to '{@event.Role}'", @event.Id);

        var modifiedBy = await _receiverRepository.GetAsync(@event.ModifiedBy, true);
        
        var excludeRecipients = new List<Guid> { user.Id };
        var administratorsMessage = $"User {user.FullName.Value} [ID: {user.Id}] role was set to '{@event.Role}' by {modifiedBy?.FullName.Value ?? "-"} [ID: {@event.ModifiedBy}]";
        
        await _notificationPublisher.PublishAsync(administratorsMessage, RecipientGroup.SystemAdministrators, excludeRecipients);
    }
}