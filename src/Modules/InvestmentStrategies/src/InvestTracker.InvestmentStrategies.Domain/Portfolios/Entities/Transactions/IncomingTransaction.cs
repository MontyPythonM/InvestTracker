using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;

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