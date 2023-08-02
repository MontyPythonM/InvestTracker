using InvestTracker.InvestmentStrategies.Domain.Collaborations.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Collaborations.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;

public class Collaboration : AggregateRoot<CollaborationId>
{
    public DateTime CreatedAt { get; set; }

    private Collaboration()
    {
    }

    private Collaboration(CollaborationId collaborationId, DateTime now)
    {
        Id = collaborationId;
        CreatedAt = now;
    }

    public static Collaboration Create(Stakeholder advisor, StakeholderId principalId, DateTime now)
    {
        if (advisor.Subscription != SystemSubscription.Advisor)
        {
            throw new CollaboratorIsNotAdvisorException(advisor.Id);
        }

        if (advisor.IsActive == false)
        {
            throw new InactiveStakeholderException(advisor.Id);
        }

        var collaborationId = new CollaborationId(advisor.Id.Value, principalId.Value);
        return new Collaboration(collaborationId, now);
    }
}