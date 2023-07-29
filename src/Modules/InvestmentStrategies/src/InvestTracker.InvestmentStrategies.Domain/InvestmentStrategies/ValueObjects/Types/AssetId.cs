using InvestTracker.Shared.Abstractions.DDD.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

public class AssetId : TypeId
{
    public AssetId(Guid value) : base(value)
    {
    }
    
    public static implicit operator AssetId(Guid id) => new(id);
}