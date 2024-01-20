using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External.Handlers;

internal sealed class UserAccountDeactivatedHandler : IEventHandler<UserAccountDeactivated>
{
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly ITimeProvider _timeProvider;

    public UserAccountDeactivatedHandler(IStakeholderRepository stakeholderRepository, ITimeProvider timeProvider)
    {
        _stakeholderRepository = stakeholderRepository;
        _timeProvider = timeProvider;
    }
    
    public async Task HandleAsync(UserAccountDeactivated @event)
    {
        var stakeholder = await _stakeholderRepository.GetAsync(@event.Id);
        if (stakeholder is null)
        {
            return;
        }

        stakeholder.Lock(_timeProvider.Current(), @event.ModifiedBy);
        await _stakeholderRepository.UpdateAsync(stakeholder);
    }
}