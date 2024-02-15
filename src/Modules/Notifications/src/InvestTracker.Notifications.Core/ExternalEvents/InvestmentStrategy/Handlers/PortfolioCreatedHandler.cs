using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.InvestmentStrategy.Handlers;

public class PortfolioCreatedHandler : IEventHandler<PortfolioCreated>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationPublisher _notificationPublisher;

    public PortfolioCreatedHandler(IReceiverRepository receiverRepository, INotificationPublisher notificationPublisher)
    {
        _receiverRepository = receiverRepository;
        _notificationPublisher = notificationPublisher;
    }
    
    public async Task HandleAsync(PortfolioCreated @event)
    {
        var owner = await _receiverRepository.GetAsync(@event.OwnerId);
        if (owner is null)
        {
            return;
        }

        var ownerNotification = new PersonalNotification(
            $"Portfolio '{@event.PortfolioTitle}' created", 
            owner.Id,
            r => r.PersonalSettings.PortfoliosActivity);
        
        var collaboratorsNotification = new PersonalNotification(
            $"Portfolio '{@event.PortfolioTitle}' owned by {owner.FullName.Value} created", 
            @event.CollaboratorIds,
            r => r.PersonalSettings.PortfoliosActivity);

        await _notificationPublisher.PublishAsync(ownerNotification);
        await _notificationPublisher.PublishAsync(collaboratorsNotification);
    }
}