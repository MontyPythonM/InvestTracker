using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.Assets;

public sealed class Cash : Asset
{
    public Cash(AssetId id, Currency currency, AssetDataId assetDataId, PortfolioId portfolioId, Note? note = null) 
        : base(id, currency, assetDataId, portfolioId, note)
    {
    }

    public Cash() : base(Guid.NewGuid(), new Currency(), Guid.NewGuid(), Guid.NewGuid())
    {
    }
}