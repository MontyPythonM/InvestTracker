using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;

public class Portfolio
{
    public PortfolioId Id { get; set; }
    public Title Title { get; private set; }
    public Note Note { get; private set; }
    public Description Description { get; private set; }
    public IEnumerable<FinancialAssetId> FinancialAssets
    {
        get => _financialAssets;
        set => _financialAssets = new HashSet<FinancialAssetId>(value);
    }
    
    private HashSet<FinancialAssetId> _financialAssets = new();
    
    private Portfolio()
    {
    }

    internal Portfolio(PortfolioId id, Title title, Note note, Description description)
    {
        Id = id;
        Title = title;
        Note = note;
        Description = description;
    }

    internal void AddFinancialAsset(FinancialAssetId assetId) => _financialAssets.Add(assetId);
    internal void RemoveFinancialAsset(FinancialAssetId assetId) => _financialAssets.Remove(assetId);
}