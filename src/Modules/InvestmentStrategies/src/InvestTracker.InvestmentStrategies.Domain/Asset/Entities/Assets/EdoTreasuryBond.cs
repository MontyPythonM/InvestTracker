using InvestTracker.InvestmentStrategies.Domain.Asset.Policies.AssetLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Entities.Assets;

public class EdoTreasuryBond : Asset
{
    private const decimal EdoUnit = 100;
    
    private EdoTreasuryBond()
    {
    }
    
    private EdoTreasuryBond(ISet<AssetId> existingPortfolioAssets, Subscription subscription, IEnumerable<IAssetLimitPolicy> policies, 
        AssetId id, Currency currency, AssetDataId assetDataId, PortfolioId portfolioId, Note note) 
        : base(existingPortfolioAssets, subscription, policies, id, currency, assetDataId, portfolioId, note)
    {
    }

    public static EdoTreasuryBond Create(ISet<AssetId> existingPortfolioAssets, Subscription subscription, IEnumerable<IAssetLimitPolicy> policies, 
        AssetId id, AssetDataId assetDataId, PortfolioId portfolioId, Note note)
    {
        return new EdoTreasuryBond(existingPortfolioAssets, subscription, policies, id, new Currency("PLN"), assetDataId, portfolioId, note);
    }

    public void Add(TransactionId transactionId, Volume volume, DateTime transactionDate, Note note)
    {
        var amount = new Amount(volume * EdoUnit);
        AddFunds(transactionId, amount, transactionDate, note);
    }

    public void Deduct(TransactionId transactionId, Volume volume, DateTime transactionDate, Note note)
    {
        var amount = new Amount(volume * EdoUnit);
        DeductFunds(transactionId, amount, transactionDate, note);
    }
    
    // todo wczesniejszy wykup, oprocentowanie 1 rok i nastepne, okres trwania obligacji
}