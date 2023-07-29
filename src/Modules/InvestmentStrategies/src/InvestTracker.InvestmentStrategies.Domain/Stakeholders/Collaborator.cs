using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders;

public sealed class Collaborator : Stakeholder
{
    public CollaborationValidity CollaborationValidity { get; private set; }
}