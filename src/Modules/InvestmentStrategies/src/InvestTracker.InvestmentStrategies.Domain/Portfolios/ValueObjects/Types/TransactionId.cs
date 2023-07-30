namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;

public record TransactionId(Guid Value)
{
    public static implicit operator Guid(TransactionId id) => id.Value;
    public static implicit operator TransactionId?(Guid id) => id.Equals(Guid.Empty) ? null : new TransactionId(id);
}