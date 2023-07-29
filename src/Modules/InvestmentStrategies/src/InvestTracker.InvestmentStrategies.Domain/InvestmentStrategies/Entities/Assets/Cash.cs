using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.Assets;

public sealed class Cash : Asset
{
    public Cash(AssetId id, Currency currency, AssetTypeId assetTypeId, PortfolioId portfolioId, Note? note = null) 
        : base(id, currency, assetTypeId, portfolioId, note)
    {
    }

    public Cash() : base(Guid.NewGuid(), new Currency(), Guid.NewGuid(), Guid.NewGuid())
    {
    }
}