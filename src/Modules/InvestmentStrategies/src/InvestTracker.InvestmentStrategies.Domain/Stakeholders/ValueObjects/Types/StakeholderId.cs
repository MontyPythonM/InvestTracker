namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

public readonly record struct StakeholderId(Guid Value)
{
    public static implicit operator Guid(StakeholderId id) => id.Value;
    public static implicit operator StakeholderId(Guid id) => new(id);
}