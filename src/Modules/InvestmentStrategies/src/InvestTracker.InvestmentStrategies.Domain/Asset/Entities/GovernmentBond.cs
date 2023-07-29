using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.AssetType.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Entities;

public sealed class GovernmentBond : Asset
{
    public Broker? Broker { get; private set; }

    public GovernmentBond(AssetId id, Currency currency, AssetTypeId assetTypeId, PortfolioId portfolioId, Note? note = null) 
        : base(id, currency, assetTypeId, portfolioId, note)
    {
    }
}