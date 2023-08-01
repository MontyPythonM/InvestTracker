namespace InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;

public record AssetId(Guid Value)
{
    public static implicit operator Guid(AssetId id) => id.Value;
    public static implicit operator AssetId?(Guid id) => id.Equals(Guid.Empty) ? null : new AssetId(id);
}