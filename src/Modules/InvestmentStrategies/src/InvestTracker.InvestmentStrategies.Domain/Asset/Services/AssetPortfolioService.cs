using InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Asset.Policies;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.AssetType.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Services;

internal class AssetPortfolioService : IAssetPortfolioService
{
    private readonly IEnumerable<IAssetLimitPolicy> _policies;

    public AssetPortfolioService(IEnumerable<IAssetLimitPolicy> policies)
    {
        _policies = policies;
    }
    
    public Entities.Asset CreateAssetInPortfolio(Portfolio portfolio, Subscription subscription, AssetId id, 
        Currency currency, AssetTypeId assetTypeId, PortfolioId portfolioId, Note? note = null)
    {
        var policy = _policies.SingleOrDefault(x => x.CanBeApplied(subscription));
        var assetsInPortfolio = portfolio.Assets.Count();

        if (policy is null)
        {
            throw new NoAssetLimitPolicyFoundException();
        }

        if (!policy.CanAddNewAsset(portfolio))
        {
            throw new AssetsLimitExceedException(assetsInPortfolio);
        }
        
        var asset = new Entities.Asset(id, currency, assetTypeId, portfolioId, note);
        portfolio.AddAsset(asset.Id);
        
        return asset;
    }
}