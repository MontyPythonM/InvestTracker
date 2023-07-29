using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.Transactions;

public abstract class Transaction
{
    public TransactionId Id { get; }
    public Amount Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public Spread? Spread { get; set; }
    public Note? Note { get; set; }

    protected Transaction()
    {
    }
    
    protected Transaction(TransactionId id, Amount amount, DateTime transactionDate, Spread? spread, Note? note)
    {
        Id = id;
        Amount = amount;
        TransactionDate = transactionDate;
        Spread = spread;
        Note = note;
    }
}