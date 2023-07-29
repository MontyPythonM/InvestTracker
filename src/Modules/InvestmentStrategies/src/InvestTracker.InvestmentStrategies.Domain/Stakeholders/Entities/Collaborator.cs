using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;

internal class Collaborator : Stakeholder
{
    public CollaborationValidity CollaborationValidity { get; private set; }

    private Collaborator()
    {
    }

    public Collaborator(StakeholderId id, FullName fullName, Email email, DateTime now, bool isActive, 
        CollaborationValidity collaborationValidity, Role? role = null, Subscription? subscription = null) 
        : base(id, fullName, email, now, isActive, role, subscription)
    {
        CollaborationValidity = collaborationValidity;
    }
}