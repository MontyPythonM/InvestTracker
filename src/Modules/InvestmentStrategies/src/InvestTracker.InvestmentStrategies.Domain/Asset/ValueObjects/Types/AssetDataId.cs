namespace InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;

public record AssetDataId(Guid Value)
{
    public static implicit operator Guid(AssetDataId id) => id.Value;
    public static implicit operator AssetDataId?(Guid id) => id.Equals(Guid.Empty) ? null : new AssetDataId(id);
}