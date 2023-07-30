using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;

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