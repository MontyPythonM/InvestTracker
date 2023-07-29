using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.Transactions;

public class IncomingTransaction : Transaction
{
    private IncomingTransaction()
    {
    }

    public IncomingTransaction(TransactionId id, Amount amount, DateTime transactionDate, Spread? spread = null, Note? note = null) 
        : base(id, amount, transactionDate, spread, note)
    {
    }
}