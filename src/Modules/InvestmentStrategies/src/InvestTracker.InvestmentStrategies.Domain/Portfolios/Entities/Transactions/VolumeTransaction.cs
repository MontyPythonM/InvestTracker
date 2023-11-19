using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;

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