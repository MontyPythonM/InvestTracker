using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Amount;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;

public class Cash : IFinancialAsset
{
    public FinancialAssetId Id { get; private set; }
    public Currency Currency { get; private set; }
    public Note Note { get; private set; }
    public IEnumerable<AmountTransaction> Transactions
    {
        get => _transactions;
        set => _transactions = new HashSet<AmountTransaction>(value);
    }
    
    private HashSet<AmountTransaction> _transactions = new();

    private Cash()
    {
    }
    
    internal Cash(FinancialAssetId id, Currency currency, Note note)
    {
        Id = id;
        Currency = currency;
        Note = note;
    }
    
    public IncomingAmountTransaction AddFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Note note)
    {
        var transaction = new IncomingAmountTransaction(transactionId, amount, transactionDate, note);
        _transactions.Add(transaction);

        return transaction;
    }

    public OutgoingAmountTransaction DeductFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Note note)
    {
        var transaction = new OutgoingAmountTransaction(transactionId, amount, transactionDate, note);
        _transactions.Add(transaction);

        return transaction;
    }
    
    public Amount GetCurrentAmount() => _transactions.OfType<IncomingAmountTransaction>().Sum(x => x.Amount) - 
                                     _transactions.OfType<OutgoingAmountTransaction>().Sum(x => x.Amount);
    
    public string GetAssetName() => $"Cash ({Currency.Value})";
}