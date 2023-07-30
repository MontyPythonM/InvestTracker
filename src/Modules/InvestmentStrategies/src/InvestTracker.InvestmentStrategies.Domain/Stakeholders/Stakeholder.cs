using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders;

public abstract class Stakeholder
{
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Role? Role { get; private set; }
    public Subscription? Subscription { get; private set; }
    public bool IsActive { get; private set; }
}