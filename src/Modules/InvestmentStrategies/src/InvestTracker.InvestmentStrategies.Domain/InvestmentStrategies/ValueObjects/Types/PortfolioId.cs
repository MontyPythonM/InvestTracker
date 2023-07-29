using InvestTracker.Shared.Abstractions.DDD.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

public class PortfolioId : TypeId
{
    public PortfolioId(Guid value) : base(value)
    {
    }
    
    public static implicit operator PortfolioId(Guid id) => new(id);

}