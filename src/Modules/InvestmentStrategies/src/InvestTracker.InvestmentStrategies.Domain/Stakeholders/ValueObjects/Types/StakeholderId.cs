namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

public record StakeholderId(Guid Value)
{
    public static implicit operator Guid(StakeholderId id) => id.Value;
    public static implicit operator StakeholderId?(Guid id) => id.Equals(Guid.Empty) ? null : new StakeholderId(id);
}