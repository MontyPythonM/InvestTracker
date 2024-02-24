using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.InvestmentStrategy.Handlers;

public class InvestmentStrategyCreatedHandler : IEventHandler<InvestmentStrategyCreated>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationPublisher _notificationPublisher;

    public InvestmentStrategyCreatedHandler(IReceiverRepository receiverRepository, INotificationPublisher notificationPublisher)
    {
        _receiverRepository = receiverRepository;
        _notificationPublisher = notificationPublisher;
    }
    
    public async Task HandleAsync(InvestmentStrategyCreated @event)
    {
        var owner = await _receiverRepository.GetAsync(@event.OwnerId);
        if (owner is null)
        {
            return;
        }
        
        var ownerNotification = new PersonalNotification(
            $"Investment strategy '{@event.InvestmentStrategyTitle}' created", 
            owner.Id,
            r => r.PersonalSettings.InvestmentStrategiesActivity);
        
        await _notificationPublisher.NotifyAsync(ownerNotification);
    }
}