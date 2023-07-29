using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Entities.Transactions;

public abstract class Transaction
{
    public TransactionId Id { get; }
    public Amount? Amount { get; private set; }
    public Volume? Volume { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public Spread? Spread { get; private set; }
    public Note? Note { get; private set; }

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
    
    protected Transaction(TransactionId id, Volume volume, DateTime transactionDate, Note? note)
    {
        Id = id;
        Volume = volume;
        TransactionDate = transactionDate;
        Note = note;
    }
}