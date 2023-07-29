using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;

internal abstract class Stakeholder
{
    public StakeholderId Id { get; private set; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Role? Role { get; private set; }
    public Subscription? Subscription { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    protected Stakeholder()
    {
    }

    protected Stakeholder(StakeholderId id, FullName fullName, Email email, DateTime now, bool isActive, 
        Role? role = null, Subscription? subscription = null)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Role = role;
        Subscription = subscription;
        IsActive = isActive;
        CreatedAt = now;
    }

    public bool Lock() => IsActive = false;
    public bool Unlock() => IsActive = true;
}