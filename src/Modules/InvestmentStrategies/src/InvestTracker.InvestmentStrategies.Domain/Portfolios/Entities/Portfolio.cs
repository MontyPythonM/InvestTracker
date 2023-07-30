using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Assets;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;

public class Portfolio : AggregateRoot<PortfolioId>
{
    public Title Title { get; private set; }
    public Note? Note { get; private set; }
    public Description? Description { get; private set; }
    public IEnumerable<Asset> Assets => _assets;

    private HashSet<Asset> _assets = new();

    private Portfolio()
    {
    }
    
    internal Portfolio(PortfolioId id, Title title, Note? note, Description? description)
    {
        Id = id;
        Title = title;
        Note = note;
        Description = description;
    }

    public void AddAsset(Asset asset, Subscription subscription, IEnumerable<IAssetLimitPolicy> policies)
    {
        var policy = policies.SingleOrDefault(policy => policy.CanBeApplied(subscription));

        if (policy is null)
        {
            throw new AssetLimitPolicyNotFoundException(subscription);
        }

        if (!policy.CanAddAsset(_assets))
        {
            throw new AssetLimitExceedException(subscription);
        }

        _assets.Add(asset);
    }

    public void RemoveAsset(AssetId assetId) => _assets.RemoveWhere(asset => asset.Id == assetId);
    public void RemoveAsset(Asset asset) => _assets.Remove(asset);
}