using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.InvestmentStrategy.Handlers;

public class FinancialAssetAddedHandler : IEventHandler<FinancialAssetAdded>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationPublisher _notificationPublisher;

    public FinancialAssetAddedHandler(IReceiverRepository receiverRepository, INotificationPublisher notificationPublisher)
    {
        _receiverRepository = receiverRepository;
        _notificationPublisher = notificationPublisher;
    }

    public async Task HandleAsync(FinancialAssetAdded @event)
    {
        var owner = await _receiverRepository.GetAsync(@event.OwnerId, true);
        if (owner is null)
        {
            return;
        }
        
        var ownerNotification = new PersonalNotification(
            $"Financial asset {@event.FinancialAssetName} was added to {@event.PortfolioTitle} portfolio", 
            owner.Id,
            setting => setting.AssetActivity);
        
        var collaboratorNotification = new PersonalNotification(
            $"Financial asset '{@event.FinancialAssetName}' was added to {@event.PortfolioTitle} portfolio owned by {owner.FullName.Value}", 
            @event.CollaboratorIds,
            setting => setting.AssetActivity);

        await _notificationPublisher.NotifyAsync(ownerNotification);
        await _notificationPublisher.NotifyAsync(collaboratorNotification);
    }
}