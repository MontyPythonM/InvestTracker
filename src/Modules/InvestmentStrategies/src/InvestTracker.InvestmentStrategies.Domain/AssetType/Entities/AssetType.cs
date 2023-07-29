using InvestTracker.InvestmentStrategies.Domain.AssetType.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.AssetType.Entities;

public abstract class AssetType
{
    public AssetTypeId Id { get; set; }
}