using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Entities.Transactions;

public class OutgoingTransaction : Transaction
{
    public Tax? Tax { get; set; }

    private OutgoingTransaction()
    {
    }
    
    public OutgoingTransaction(TransactionId id, Amount amount, DateTime transactionDate, Spread? spread = null, Note? note = null) 
        : base(id, amount, transactionDate, spread, note)
    {
    }
}