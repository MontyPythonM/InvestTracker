namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;

public readonly record struct TransactionId(Guid Value)
{
    public static implicit operator Guid(TransactionId id) => id.Value;
    public static implicit operator TransactionId(Guid id) => new(id);
}