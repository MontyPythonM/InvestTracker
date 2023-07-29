using InvestTracker.Shared.Abstractions.DDD.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

public class TransactionId : TypeId
{
    public TransactionId(Guid value) : base(value)
    {
    }
    
    public static implicit operator TransactionId(Guid id) => new(id);
}