﻿using InvestTracker.InvestmentStrategies.Domain.Collaborations.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;

public class Collaboration : AggregateRoot
{
    public StakeholderId AdvisorId { get; }
    public StakeholderId PrincipalId { get; }   

    private Collaboration()
    {
    }
    
    private Collaboration(StakeholderId advisorId, StakeholderId principalId, DateTime now)
    {
        AdvisorId = advisorId;
        PrincipalId = principalId;
        SetCreation(now, advisorId);
    }

    public static Collaboration Create(Stakeholder advisor, Stakeholder principal, DateTime now)
    {
        if (advisor.Id.Value == principal.Id.Value)
        {
            throw new InvalidCollaborationException(advisor.Id);
        }
        
        if (advisor.Subscription != SystemSubscription.Advisor)
        {
            throw new CollaboratorIsNotAdvisorException(advisor.Id);
        }

        if (advisor.IsActive == false)
        {
            throw new InactiveStakeholderException(advisor.Id);
        }

        return new Collaboration(advisor.Id.Value, principal.Id.Value, now);
    }
}