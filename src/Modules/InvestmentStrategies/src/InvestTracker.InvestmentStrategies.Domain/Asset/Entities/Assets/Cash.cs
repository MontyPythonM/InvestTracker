using InvestTracker.InvestmentStrategies.Domain.Asset.Policies.AssetLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Entities.Assets;

public sealed class Cash : Asset
{
    private Cash()
    {
    }
    
    public Cash(ISet<AssetId> existingPortfolioAssets, Subscription subscription, IEnumerable<IAssetLimitPolicy> policies, 
        AssetId id, Currency currency, AssetDataId assetDataId, PortfolioId portfolioId, Note note) 
        : base(existingPortfolioAssets, subscription, policies, id, currency, assetDataId, portfolioId, note)
    {
    }
}