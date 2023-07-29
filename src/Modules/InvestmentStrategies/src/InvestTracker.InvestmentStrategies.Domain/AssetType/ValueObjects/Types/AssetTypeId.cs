namespace InvestTracker.InvestmentStrategies.Domain.AssetType.ValueObjects.Types;

public record AssetTypeId(Guid Value)
{
    public static implicit operator Guid(AssetTypeId id) => id.Value;
    public static implicit operator AssetTypeId?(Guid id) => id.Equals(Guid.Empty) ? null : new AssetTypeId(id);
}