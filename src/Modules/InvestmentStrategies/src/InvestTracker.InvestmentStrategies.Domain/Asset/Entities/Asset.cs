using InvestTracker.InvestmentStrategies.Domain.Asset.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Asset.Events;
using InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Asset.Policies.AssetLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Entities;

public abstract class Asset : AggregateRoot<AssetId>
{
    public Note Note { get; private set; }
    public Currency Currency { get; private set; }
    public AssetDataId AssetDataId { get; private set; }
    public PortfolioId PortfolioId { get; private set; }
    public IEnumerable<Transaction> Transactions => _transactions;
    
    private HashSet<Transaction> _transactions = new();
    
    protected Asset()
    {
    }
    
    protected Asset(ISet<AssetId> existingPortfolioAssets, Subscription subscription, IEnumerable<IAssetLimitPolicy> policies, 
        AssetId id, Currency currency, AssetDataId assetDataId, PortfolioId portfolioId, Note note)
    {
        var policy = policies.SingleOrDefault(policy => policy.CanBeApplied(subscription));

        if (policy is null)
        {
            throw new AssetLimitPolicyNotFoundException(subscription);
        }

        if (!policy.CanAddAsset(existingPortfolioAssets))
        {
            throw new AssetLimitExceedException(subscription);
        }
        
        Id = id;
        Currency = currency;
        AssetDataId = assetDataId;
        PortfolioId = portfolioId;
        Note = note;
        
        AddEvent(new AssetInPortfolioAdded(id, portfolioId, assetDataId));
    }

    protected IncomingTransaction AddFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Note note)
    {
        var transaction = new IncomingTransaction(transactionId, amount, transactionDate, note);
        _transactions.Add(transaction);

        return transaction;
    }

    protected OutgoingTransaction DeductFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Note note)
    {
        var transaction = new OutgoingTransaction(transactionId, amount, transactionDate, note);
        _transactions.Add(transaction);

        return transaction;
    }
}