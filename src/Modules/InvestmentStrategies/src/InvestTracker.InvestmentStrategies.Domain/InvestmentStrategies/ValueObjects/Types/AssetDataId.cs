using InvestTracker.Shared.Abstractions.DDD.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

public class AssetDataId : TypeId
{
    public AssetDataId(Guid value) : base(value)
    {
    }
    
    public static implicit operator AssetDataId(Guid id) => new(id);
}