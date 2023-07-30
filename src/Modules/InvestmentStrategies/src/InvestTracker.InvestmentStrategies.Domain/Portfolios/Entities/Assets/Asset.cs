using InvestTracker.InvestmentStrategies.Domain.AssetType.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Assets;

public abstract class Asset
{
    public AssetId Id { get; private set; }
    public Note? Note { get; private set; }
    public Currency Currency { get; private set; }
    public AssetTypeId AssetTypeId { get; private set; }
    public Portfolio? Portfolio { get; private set; }
    public IEnumerable<Transaction> Transactions => _transactions;
    
    private HashSet<Transaction> _transactions = new();

    protected Asset(AssetId id, Currency currency, AssetTypeId assetTypeId, Note? note = null)
    {
        Id = id;
        Currency = currency;
        AssetTypeId = assetTypeId;
        Note = note;
    }

    internal IncomingTransaction AddFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Spread? spread = null, Note? note = null)
    {
        var transaction = new IncomingTransaction(transactionId, amount, transactionDate, spread, note);
        _transactions.Add(transaction);

        return transaction;
    }

    internal OutgoingTransaction DeductFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Spread? spread = null, Note? note = null)
    {
        var transaction = new OutgoingTransaction(transactionId, amount, transactionDate, spread, note);
        _transactions.Add(transaction);

        return transaction;
    }
}