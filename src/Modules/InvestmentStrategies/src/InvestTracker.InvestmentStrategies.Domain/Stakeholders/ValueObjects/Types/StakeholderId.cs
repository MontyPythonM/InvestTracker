using InvestTracker.Shared.Abstractions.DDD.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

public class StakeholderId : TypeId
{
    public StakeholderId(Guid value) : base(value)
    {
    }

    public static implicit operator StakeholderId(Guid id) => new(id);
}