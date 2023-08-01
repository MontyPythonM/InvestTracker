using InvestTracker.InvestmentStrategies.Application.Stakeholders.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities.Stakeholders;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External.Handlers;

internal sealed class CollaborationStartedHandler : IEventHandler<CollaborationStarted>
{
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly ICollaboratorRepository _collaboratorRepository;

    public CollaborationStartedHandler(IStakeholderRepository stakeholderRepository, 
        ICollaboratorRepository collaboratorRepository)
    {
        _stakeholderRepository = stakeholderRepository;
        _collaboratorRepository = collaboratorRepository;
    }
    
    public async Task HandleAsync(CollaborationStarted @event)
    {
        var collaboratorExists = await _collaboratorRepository.ExistsAsync(@event.AdvisorId);
        if (collaboratorExists)
        {
            return;
        }

        var advisor = await _stakeholderRepository.GetAsync(@event.AdvisorId);
        var investor = await _stakeholderRepository.GetAsync(@event.InvestorId);

        if (investor is null)
        {
            throw new StakeholderNotFoundException(@event.InvestorId);
        }
        
        var collaborator = Collaborator.Create(advisor, investor.Id);
        await _collaboratorRepository.AddAsync(collaborator);
    }
}