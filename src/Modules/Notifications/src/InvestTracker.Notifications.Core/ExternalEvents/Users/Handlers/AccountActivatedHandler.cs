﻿using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class AccountActivatedHandler : IEventHandler<AccountActivated>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly INotificationPublisher _notificationPublisher;

    public AccountActivatedHandler(IReceiverRepository receiverRepository, INotificationPublisher notificationPublisher)
    {
        _receiverRepository = receiverRepository;
        _notificationPublisher = notificationPublisher;
    }

    public async Task HandleAsync(AccountActivated @event)
    {
        var user = await _receiverRepository.GetAsync(@event.Id, null, true);
        var modifiedBy = await _receiverRepository.GetAsync(@event.ModifiedBy, null, true);

        var groupNotification = new GroupNotification(
            $"{user?.FullName?.Value ?? "-"} account was activated by {modifiedBy?.FullName?.Value ?? "-"}", 
            RecipientGroup.SystemAdministrators,
            r => r.PersonalSettings.AdministratorsActivity);
        
        await _notificationPublisher.PublishAsync(groupNotification);
    }
}