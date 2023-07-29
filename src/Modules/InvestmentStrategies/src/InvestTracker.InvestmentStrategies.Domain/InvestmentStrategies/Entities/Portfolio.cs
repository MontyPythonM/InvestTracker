using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.Assets;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;

public class Portfolio
{
    public PortfolioId Id { get; private set; }
    public Title Title { get; private set; }
    public Note? Note { get; private set; }
    public Description? Description { get; private set; }
    public InvestmentStrategy InvestmentStrategy { get; private set; }
    public IEnumerable<Asset> Assets => _assets;

    private HashSet<Asset> _assets = new();
    
    internal Portfolio(PortfolioId id, Title title, Note? note, Description? description)
    {
        Id = id;
        Title = title;
        Note = note;
        Description = description;
    }
    
    public void AddAsset(Asset asset) => _assets.Add(asset);
}