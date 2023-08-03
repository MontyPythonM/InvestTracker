using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;

public class Stakeholder : AggregateRoot<StakeholderId>
{
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Subscription? Subscription { get; private set; }
    public Role? Role { get; private set; }
    public bool IsActive { get; private set; }

    protected Stakeholder()
    {
    }

    public Stakeholder(StakeholderId id, FullName fullName, Email email, Subscription? subscription, Role? role = null)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Subscription = subscription;
        Role = role;
        IsActive = true;
    }

    public void Lock() => IsActive = false;
    public void Unlock() => IsActive = true;
}