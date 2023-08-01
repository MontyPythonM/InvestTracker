using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities.Stakeholders;

public sealed class Collaborator : AggregateRoot<CollaboratorId>
{
    public StakeholderId PrincipalId { get; set; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }

    private Collaborator()
    {
    }

    private Collaborator(CollaboratorId id, StakeholderId principalId, FullName fullName, Email email)
    {
        Id = id;        
        PrincipalId = principalId;
        FullName = fullName;
        Email = email;
    }

    public static Collaborator Create(Stakeholder advisor, StakeholderId principalId)
    {
        if (advisor.Subscription != SystemSubscription.Advisor)
        {
            throw new CollaboratorIsNotAdvisorException(advisor.Id);
        }

        if (advisor.IsActive == false)
        {
            throw new InactiveStakeholderException(advisor.Id);
        }

        return new Collaborator(advisor.Id.Value, principalId, advisor.FullName, advisor.Email);
    }
}