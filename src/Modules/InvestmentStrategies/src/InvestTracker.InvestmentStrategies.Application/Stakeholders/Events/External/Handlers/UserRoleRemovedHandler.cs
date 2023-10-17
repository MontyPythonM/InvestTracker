using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External.Handlers;

internal sealed class UserRoleRemovedHandler : IEventHandler<UserRoleRemoved>
{
    private readonly IStakeholderRepository _stakeholderRepository;

    public UserRoleRemovedHandler(IStakeholderRepository stakeholderRepository)
    {
        _stakeholderRepository = stakeholderRepository;
    }

    public async Task HandleAsync(UserRoleRemoved @event)
    {
        var stakeholder = await _stakeholderRepository.GetAsync(@event.Id);
        if (stakeholder is null)
        {
            return;
        }

        stakeholder.SetRole(SystemRole.None);
        await _stakeholderRepository.UpdateAsync(stakeholder);
    }
}