using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Volume;

public sealed class OutgoingVolumeTransaction : VolumeTransaction
{
    private OutgoingVolumeTransaction()
    {
    }
    
    public OutgoingVolumeTransaction(TransactionId id, ValueObjects.Volume volume, DateTime transactionDate, Note note) 
        : base(id, volume, transactionDate, note)
    {
    }
}