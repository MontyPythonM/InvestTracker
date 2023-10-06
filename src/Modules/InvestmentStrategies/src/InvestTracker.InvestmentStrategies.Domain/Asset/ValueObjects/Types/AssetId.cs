using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;

public class AssetId : TypeId
{
    private AssetId()
    {
    }
    
    public AssetId(Guid value) : base(value)
    {
    }
    
    public static implicit operator Guid(AssetId id) => id.Value;
    public static implicit operator AssetId(Guid id) => new(id);
}

// public record AssetId(Guid Value)
// {
//     public static implicit operator Guid(AssetId id) => id.Value;
//     public static implicit operator AssetId(Guid id) => new(id);
// }