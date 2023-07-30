using InvestTracker.InvestmentStrategies.Domain.AssetType.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.AssetType.Entities;

public abstract class AssetType
{
    public AssetTypeId Id { get; set; }
}