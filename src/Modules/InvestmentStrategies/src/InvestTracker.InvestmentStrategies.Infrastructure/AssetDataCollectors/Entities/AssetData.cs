using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Infrastructure.AssetDataCollectors.Entities;

public abstract class AssetData
{
    public AssetDataId Id { get; set; }
}