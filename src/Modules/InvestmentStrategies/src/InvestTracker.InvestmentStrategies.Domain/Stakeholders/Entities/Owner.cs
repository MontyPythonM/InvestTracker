using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;

internal class Owner : Stakeholder
{
    private Owner()
    {
    }

    public Owner(StakeholderId id, FullName fullName, Email email, DateTime now, bool isActive, 
        Role? role = null, Subscription? subscription = null) 
        : base(id, fullName, email, now, isActive, role, subscription)
    {
    }
}