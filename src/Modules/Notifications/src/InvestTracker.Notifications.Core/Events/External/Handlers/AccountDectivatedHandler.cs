using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class AccountDeactivatedHandler : IEventHandler<AccountDeactivated>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationPublisher _notificationPublisher;

    public AccountDeactivatedHandler(IReceiverRepository receiverRepository, INotificationPublisher notificationPublisher)
    {
        _receiverRepository = receiverRepository;
        _notificationPublisher = notificationPublisher;
    }
    
    public async Task HandleAsync(AccountDeactivated @event)
    {
        var user = await _receiverRepository.GetAsync(@event.Id, true);
        var modifiedBy = await _receiverRepository.GetAsync(@event.ModifiedBy, true);

        var userName = user?.FullName ?? "-";
        var modifiedByName = modifiedBy?.FullName ?? "-";
        
        var message = $"{userName.Value} account was deactivated by {modifiedByName.Value}";
        
        await _notificationPublisher.PublishAsync(message, RecipientGroup.SystemAdministrators);
    }
}