using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.Assets;

public abstract class Asset
{
    public AssetId Id { get; private set; }
    public Note? Note { get; private set; }
    public Currency Currency { get; private set; }
    public AssetTypeId AssetTypeId { get; private set; }
    public PortfolioId PortfolioId { get; private set; }
    public IEnumerable<Transaction> Transactions => _transactions;
    
    private HashSet<Transaction> _transactions = new();

    protected Asset(AssetId id, Currency currency, AssetTypeId assetTypeId, PortfolioId portfolioId, Note? note = null)
    {
        Id = id;
        Currency = currency;
        AssetTypeId = assetTypeId;
        PortfolioId = portfolioId;
        Note = note;
    }

    public IncomingTransaction AddFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Spread? spread = null, Note? note = null)
    {
        if (amount <= 0)
        {
            throw new InvalidTransactionAmountException(amount);
        }

        var transaction = new IncomingTransaction(transactionId, amount, transactionDate, spread, note);
        _transactions.Add(transaction);

        return transaction;
    }

    public OutgoingTransaction DeductFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Spread? spread = null, Note? note = null)
    {
        if (amount <= 0)
        {
            throw new InvalidTransactionAmountException(amount);
        }
        
        var transaction = new OutgoingTransaction(transactionId, amount, transactionDate, spread, note);
        _transactions.Add(transaction);

        return transaction;
    }
}