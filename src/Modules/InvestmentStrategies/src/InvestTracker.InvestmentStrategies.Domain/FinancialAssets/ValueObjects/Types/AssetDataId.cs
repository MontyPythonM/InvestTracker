namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;

public readonly record struct AssetDataId(Guid Value)
{
    public static implicit operator Guid(AssetDataId id) => id.Value;
    public static implicit operator AssetDataId(Guid id) => new(id);
}