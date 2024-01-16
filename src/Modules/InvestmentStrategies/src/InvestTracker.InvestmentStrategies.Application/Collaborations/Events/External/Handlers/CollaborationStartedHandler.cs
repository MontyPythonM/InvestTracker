using InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;
using InvestTracker.InvestmentStrategies.Domain.Collaborations.Repositories;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.InvestmentStrategies.Application.Collaborations.Events.External.Handlers;

internal sealed class CollaborationStartedHandler : IEventHandler<CollaborationStarted>
{
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly ICollaborationRepository _collaborationRepository;
    private readonly ITimeProvider _timeProvider;

    public CollaborationStartedHandler(IStakeholderRepository stakeholderRepository, 
        ICollaborationRepository collaborationRepository, ITimeProvider timeProvider)
    {
        _stakeholderRepository = stakeholderRepository;
        _collaborationRepository = collaborationRepository;
        _timeProvider = timeProvider;
    }
    
    public async Task HandleAsync(CollaborationStarted @event)
    {
        var investorId = new StakeholderId(@event.InvestorId);
        var advisorId = new StakeholderId(@event.AdvisorId);
        
        var collaborationExists = await _collaborationRepository.ExistsAsync(advisorId, investorId);
        if (collaborationExists)
        {
            return;
        }

        var investor = await _stakeholderRepository.GetAsync(investorId);
        if (investor is null)
        {
            throw new StakeholderNotFoundException(investorId);
        }
        
        var advisor = await _stakeholderRepository.GetAsync(advisorId);
        if (advisor is null)
        {
            throw new StakeholderNotFoundException(advisorId);
        }
        
        var collaboration = Collaboration.Create(advisor, investor, _timeProvider.Current());
        await _collaborationRepository.AddAsync(collaboration);
    }
}