using InvestTracker.InvestmentStrategies.Domain.AssetType.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Assets;

public sealed class GovernmentBond : Asset
{
    public Broker? Broker { get; private set; }

    public GovernmentBond(AssetId id, Currency currency, AssetTypeId assetTypeId, Note? note = null) 
        : base(id, currency, assetTypeId, note)
    {
    }
}