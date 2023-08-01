using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;

public class Portfolio
{
    public PortfolioId PortfolioId { get; set; }
    public Title Title { get; private set; }
    public Note? Note { get; private set; }
    public Description? Description { get; private set; }
    public IEnumerable<AssetId> Assets => _assets;

    private HashSet<AssetId> _assets = new();

    private Portfolio()
    {
    }

    internal Portfolio(PortfolioId portfolioId, Title title, Note? note, Description? description)
    {
        PortfolioId = portfolioId;
        Title = title;
        Note = note;
        Description = description;
    }

    internal void AddAsset(AssetId assetId) => _assets.Add(assetId);
    internal void RemoveAsset(AssetId assetId) => _assets.Remove(assetId);
}