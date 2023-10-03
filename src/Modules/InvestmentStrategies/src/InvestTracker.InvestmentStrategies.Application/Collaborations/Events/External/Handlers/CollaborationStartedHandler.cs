﻿using InvestTracker.InvestmentStrategies.Application.Stakeholders.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;
using InvestTracker.InvestmentStrategies.Domain.Collaborations.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Collaborations.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.InvestmentStrategies.Application.Collaborations.Events.External.Handlers;

internal sealed class CollaborationStartedHandler : IEventHandler<CollaborationStarted>
{
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly ICollaborationRepository _collaborationRepository;
    private readonly ITime _time;

    public CollaborationStartedHandler(IStakeholderRepository stakeholderRepository, 
        ICollaborationRepository collaborationRepository, ITime time)
    {
        _stakeholderRepository = stakeholderRepository;
        _collaborationRepository = collaborationRepository;
        _time = time;
    }
    
    public async Task HandleAsync(CollaborationStarted @event)
    {
        var collaborationId = new CollaborationId(@event.AdvisorId, @event.InvestorId);
        
        var collaborationExists = await _collaborationRepository.ExistsAsync(collaborationId);
        if (collaborationExists)
        {
            return;
        }

        var investor = await _stakeholderRepository.GetAsync(@event.InvestorId);
        if (investor is null)
        {
            throw new StakeholderNotFoundException(@event.InvestorId);
        }
        
        var advisor = await _stakeholderRepository.GetAsync(@event.AdvisorId);
        if (advisor is null)
        {
            throw new StakeholderNotFoundException(@event.AdvisorId);
        }
        
        var collaboration = Collaboration.Create(advisor, investor.Id, _time.Current());
        await _collaborationRepository.AddAsync(collaboration);
    }
}