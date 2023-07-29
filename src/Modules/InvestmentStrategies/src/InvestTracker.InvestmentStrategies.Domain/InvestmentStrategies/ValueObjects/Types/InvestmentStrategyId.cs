using InvestTracker.Shared.Abstractions.DDD.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

public class InvestmentStrategyId : TypeId
{
    public InvestmentStrategyId(Guid value) : base(value)
    {
    }
    
    public static implicit operator InvestmentStrategyId(Guid id) => new(id);
}