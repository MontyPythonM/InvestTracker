using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class AccountCreatedHandler : IEventHandler<AccountCreated>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly ITimeProvider _timeProvider;

    public AccountCreatedHandler(IReceiverRepository receiverRepository, ITimeProvider timeProvider)
    {
        _receiverRepository = receiverRepository;
        _timeProvider = timeProvider;
    }

    public async Task HandleAsync(AccountCreated @event)
    {
        var receiverExists = await _receiverRepository.ExistsAsync(@event.Id);

        if (receiverExists)
        {
            return;
        }

        var receiver = new Receiver
        {
            Id = @event.Id,
            Email = @event.Email,
            PhoneNumber = @event.PhoneNumber,
            Role = @event.Role,
            Subscription = @event.Subscription,
            NotificationSetup = new PersonalNotificationSetup
            {
                Id = Guid.NewGuid(),
                EnableNotification = true,
                EnableEmail = true,
                EnableAdministrative = true,
                CreatedAt = _timeProvider.Current(),
                ModifiedAt = null
            }
        };

        await _receiverRepository.CreateAsync(receiver);
    }
}