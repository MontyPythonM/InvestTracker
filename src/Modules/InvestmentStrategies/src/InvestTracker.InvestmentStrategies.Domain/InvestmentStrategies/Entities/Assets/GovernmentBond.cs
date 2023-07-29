using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.Assets;

public sealed class GovernmentBond : Asset
{
    public Broker? Broker { get; private set; }

    public GovernmentBond(AssetId id, Currency currency, AssetDataId assetDataId, PortfolioId portfolioId, Note? note = null) 
        : base(id, currency, assetDataId, portfolioId, note)
    {
    }
}