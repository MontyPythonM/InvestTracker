using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class AccountDeactivatedHandler(
    IReceiverRepository receiverRepository,
    INotificationPublisher notificationPublisher)
    : IEventHandler<AccountDeactivated>
{
    public async Task HandleAsync(AccountDeactivated @event)
    {
        var user = await receiverRepository.GetAsync(@event.Id, true);
        var modifiedBy = await receiverRepository.GetAsync(@event.ModifiedBy, true);
        
        var groupNotification = new GroupNotification(
            $"{user?.FullName.Value ?? "-"} account was deactivated by {modifiedBy?.FullName.Value ?? "-"}", 
            RecipientGroup.SystemAdministrators,
            setting => setting.AdministratorsActivity);
        
        await notificationPublisher.NotifyAsync(groupNotification);
    }
}