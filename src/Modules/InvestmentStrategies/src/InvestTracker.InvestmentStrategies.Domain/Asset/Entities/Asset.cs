using InvestTracker.InvestmentStrategies.Domain.Asset.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Asset.Events;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.AssetType.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Entities;

public abstract class Asset : AggregateRoot<AssetId>
{
    public PortfolioId PortfolioId { get; private set; }
    public AssetTypeId AssetTypeId { get; private set; }
    public Note? Note { get; private set; }
    public Currency Currency { get; private set; }
    public IEnumerable<Transaction> Transactions => _transactions;
    public IEnumerable<StakeholderId> Stakeholders { get; set; }
    
    private HashSet<Transaction> _transactions = new();
    private HashSet<StakeholderId> _stakeholders = new();

    // todo trzeba ogarnąc serwis sprawdzający czy liczba assetów w portfolio nie przekracza limitu z policy
    protected Asset(AssetId id, Currency currency, AssetTypeId assetTypeId, PortfolioId portfolioId, Note? note = null)
    {
        Id = id;
        Currency = currency;
        AssetTypeId = assetTypeId;
        PortfolioId = portfolioId;
        Note = note;
        
        // przeniesc event do serwisu
        AddEvent(new AssetAdded(id, portfolioId));
    }

    public void AddStakeholder(StakeholderId id) => _stakeholders.Add(id);
    public void RemoveStakeholder(StakeholderId id) => _stakeholders.Remove(id);

    public IncomingTransaction Add(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Spread? spread = null, Note? note = null)
    {
        var transaction = new IncomingTransaction(transactionId, amount, transactionDate, spread, note);
        _transactions.Add(transaction);

        return transaction;
    }
    
    public IncomingTransaction Add(TransactionId transactionId, Volume volume, DateTime transactionDate, 
        Note? note = null)
    {
        var transaction = new IncomingTransaction(transactionId, volume, transactionDate, note);
        _transactions.Add(transaction);

        return transaction;
    }

    public OutgoingTransaction Deduct(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Spread? spread = null, Note? note = null)
    {
        var transaction = new OutgoingTransaction(transactionId, amount, transactionDate, spread, note);
        _transactions.Add(transaction);

        return transaction;
    }
    
    public OutgoingTransaction Deduct(TransactionId transactionId, Volume volume, DateTime transactionDate, 
        Spread? spread = null, Note? note = null)
    {
        var transaction = new OutgoingTransaction(transactionId, volume, transactionDate, note);
        _transactions.Add(transaction);

        return transaction;
    }
}