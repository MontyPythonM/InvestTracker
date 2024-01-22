using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class InvestorCreatedHandler : IEventHandler<InvestorCreated>
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly ITimeProvider _timeProvider;

    public InvestorCreatedHandler(IReceiverRepository receiverRepository, ITimeProvider timeProvider)
    {
        _receiverRepository = receiverRepository;
        _timeProvider = timeProvider;
    }

    public async Task HandleAsync(InvestorCreated @event)
    {
        var receiverExists = await _receiverRepository.ExistsAsync(@event.Id);

        if (receiverExists)
        {
            return;
        }

        var receiver = new Receiver
        {
            Id = Guid.NewGuid(),
            Email = @event.Email,
            PhoneNumber = @event.PhoneNumber,
            Role = SystemRole.None,
            Subscription = SystemSubscription.StandardInvestor,
            NotificationSetup = new PersonalNotificationSetup
            {
                Id = Guid.NewGuid(),
                Push = true,
                Email = true,
                Administrative = true,
                CreatedAt = _timeProvider.Current(),
                ModifiedAt = null
            }
        };

        await _receiverRepository.CreateAsync(receiver);
    }
}