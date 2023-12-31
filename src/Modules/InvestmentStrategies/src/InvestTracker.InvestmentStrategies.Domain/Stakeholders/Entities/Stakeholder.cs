﻿using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Exceptions;
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

    public Stakeholder(StakeholderId id, FullName fullName, Email email, Subscription subscription)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Subscription = subscription;
        IsActive = true;
    }

    public void SetRole(Role role)
    {
        if (!IsActive)
        {
            throw new InactiveStakeholderException(Id);
        }

        Role = role;
        IncrementVersion();
    }

    public void SetSubscription(Subscription subscription)
    {
        if (!IsActive)
        {
            throw new InactiveStakeholderException(Id);
        }
        
        Subscription = subscription;
        IncrementVersion();
    }

    public void Lock()
    {
        IsActive = false;
        IncrementVersion();
    }

    public void Unlock()
    {
        IsActive = true;
        IncrementVersion();
    }
}