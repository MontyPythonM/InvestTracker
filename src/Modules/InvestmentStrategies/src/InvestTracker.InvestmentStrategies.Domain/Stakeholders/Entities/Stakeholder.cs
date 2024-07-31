using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;

public class Stakeholder : AggregateRoot<StakeholderId>
{
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Subscription Subscription { get; private set; } = SystemSubscription.None;
    public Role Role { get; private set; } = SystemRole.None;
    public bool IsActive { get; private set; }

    protected Stakeholder()
    {
    }

    public Stakeholder(StakeholderId id, FullName fullName, Email email, Subscription subscription, DateTime createdAt)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Subscription = subscription;
        IsActive = true;
        SetCreation(createdAt, id);
    }

    public void SetRole(Role role, DateTime modifiedAt, Guid modifiedBy)
    {
        Role = role;
        SetModification(modifiedAt, modifiedBy);
        IncrementVersion();
    }

    public void SetSubscription(Subscription subscription, DateTime modifiedAt, Guid modifiedBy)
    {
        Subscription = subscription;
        SetModification(modifiedAt, modifiedBy);
        IncrementVersion();
    }

    public void Lock(DateTime modifiedAt, Guid modifiedBy)
    {
        IsActive = false;
        SetModification(modifiedAt, modifiedBy);
        IncrementVersion();
    }

    public void Unlock(DateTime modifiedAt, Guid modifiedBy)
    {
        IsActive = true;
        SetModification(modifiedAt, modifiedBy);
        IncrementVersion();
    }
}