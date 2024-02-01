using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

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
        if (user is null)
        {
            return;
        }
        
        await _notificationPublisher.PublishAsync($"Your role was set to '{SystemRole.None}'", user.Id);

        var modifiedBy = await _receiverRepository.GetAsync(@event.ModifiedBy, true);
        
        var excludeRecipients = new List<Guid> { user.Id };
        var administratorsMessage = $"User {user.FullName} [ID: {@event.Id}] role was set to '{SystemRole.None}' by {modifiedBy?.FullName ?? "-"} [ID: {@event.ModifiedBy}]";
        
        await _notificationPublisher.PublishAsync(administratorsMessage, RecipientGroup.SystemAdministrators, excludeRecipients);
    }
}