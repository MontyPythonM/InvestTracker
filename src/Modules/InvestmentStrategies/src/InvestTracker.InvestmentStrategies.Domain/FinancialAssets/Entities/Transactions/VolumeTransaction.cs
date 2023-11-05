using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Transactions;

public abstract class VolumeTransaction
{
    public TransactionId Id { get; }
    public ValueObjects.Volume Volume { get; set; }
    public DateTime TransactionDate { get; set; }
    public Note Note { get; set; }

    protected VolumeTransaction()
    {
    }
    
    protected VolumeTransaction(TransactionId id, ValueObjects.Volume volume, DateTime transactionDate, Note note)
    {
        Id = id;
        Volume = volume;
        TransactionDate = transactionDate;
        Note = note;
    }
}