using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.AssetData.Entities;

public abstract class AssetData
{
    public AssetDataId Id { get; set; }
}