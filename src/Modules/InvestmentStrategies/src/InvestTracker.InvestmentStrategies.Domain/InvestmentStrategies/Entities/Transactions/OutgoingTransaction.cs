using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.Transactions;

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