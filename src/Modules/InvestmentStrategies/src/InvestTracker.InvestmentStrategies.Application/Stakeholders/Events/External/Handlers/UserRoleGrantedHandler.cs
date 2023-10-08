using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External.Handlers;

internal sealed class UserRoleGrantedHandler : IEventHandler<UserRoleGranted>
{
    private readonly IStakeholderRepository _stakeholderRepository;

    public UserRoleGrantedHandler(IStakeholderRepository stakeholderRepository)
    {
        _stakeholderRepository = stakeholderRepository;
    }

    public async Task HandleAsync(UserRoleGranted @event)
    {
        var stakeholder = await _stakeholderRepository.GetAsync(@event.Id);
        if (stakeholder is null)
        {
            return;
        }

        stakeholder.SetRole(@event.Role);
        await _stakeholderRepository.UpdateAsync(stakeholder);
    }
}