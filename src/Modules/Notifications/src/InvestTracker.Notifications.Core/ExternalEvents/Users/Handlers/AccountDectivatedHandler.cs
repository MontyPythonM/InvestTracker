using InvestTracker.Notifications.Core.Dto;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

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
        var user = await _receiverRepository.GetAsync(@event.Id, null, true);
        var modifiedBy = await _receiverRepository.GetAsync(@event.ModifiedBy, null, true);
        
        var groupNotification = new GroupNotification(
            $"{user?.FullName.Value ?? "-"} account was deactivated by {modifiedBy?.FullName.Value ?? "-"}", 
            RecipientGroup.SystemAdministrators,
            r => r.PersonalSettings.AdministratorsActivity);
        
        await _notificationPublisher.NotifyAsync(groupNotification);
    }
}