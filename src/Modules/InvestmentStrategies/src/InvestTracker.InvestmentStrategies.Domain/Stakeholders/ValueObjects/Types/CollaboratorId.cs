namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

public record CollaboratorId(Guid Value)
{
    public static implicit operator Guid(CollaboratorId id) => id.Value;
    public static implicit operator CollaboratorId(Guid id) => new(id);
}