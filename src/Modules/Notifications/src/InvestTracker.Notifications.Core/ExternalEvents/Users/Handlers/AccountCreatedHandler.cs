using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Notifications.Core.ExternalEvents.Users.Handlers;

internal sealed class AccountCreatedHandler(
    IReceiverRepository receiverRepository, 
    ITimeProvider timeProvider)
    : IEventHandler<AccountCreated>
{
    public async Task HandleAsync(AccountCreated @event)
    {
        var receiverExists = await receiverRepository.ExistsAsync(@event.Id);

        if (receiverExists)
        {
            return;
        }

        var receiver = new Receiver
        {
            Id = @event.Id,
            FullName = @event.FullName,
            Email = @event.Email,
            PhoneNumber = @event.PhoneNumber,
            Role = @event.Role,
            Subscription = @event.Subscription,
            PersonalSettings = new PersonalSettings
            {
                Id = Guid.NewGuid(),
                CreatedAt = timeProvider.Current(),
            }
        };

        await receiverRepository.CreateAsync(receiver);
    }
}