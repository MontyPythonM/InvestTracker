﻿using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.InvestmentStrategy.Handlers;

public class InvestmentStrategySharedHandler : IEventHandler<InvestmentStrategyShared>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationPublisher _notificationPublisher;

    public InvestmentStrategySharedHandler(IReceiverRepository receiverRepository, INotificationPublisher notificationPublisher)
    {
        _receiverRepository = receiverRepository;
        _notificationPublisher = notificationPublisher;
    }
    
    public async Task HandleAsync(InvestmentStrategyShared @event)
    {
        var owner = await _receiverRepository.GetAsync(@event.OwnerId, true);
        if (owner is null)
        {
            return;
        }

        var collaborator = await _receiverRepository.GetAsync(@event.CollaboratorId, true);

        var ownerNotification = new PersonalNotification(
            $"Your investment strategy '{@event.InvestmentStrategyTitle}' was shared with {collaborator?.FullName.Value ?? "-"}", 
            owner.Id,
            setting => setting.InvestmentStrategiesActivity);
        
        var collaboratorNotification = new PersonalNotification(
            $"{owner.FullName.Value} has shared you his investment strategy '{@event.InvestmentStrategyTitle}' [ID: {@event.InvestmentStrategyId}]", 
            @event.CollaboratorId,
            setting => setting.InvestmentStrategiesActivity);

        await _notificationPublisher.NotifyAsync(ownerNotification);
        await _notificationPublisher.NotifyAsync(collaboratorNotification);
    }
}