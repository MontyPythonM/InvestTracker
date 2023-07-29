using InvestTracker.Shared.Abstractions.DDD.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

public class AssetTypeId : TypeId
{
    public AssetTypeId(Guid value) : base(value)
    {
    }
    
    public static implicit operator AssetTypeId(Guid id) => new(id);
}