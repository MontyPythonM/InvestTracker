using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Entities.Transactions;

public class IncomingTransaction : Transaction
{
    private IncomingTransaction()
    {
    }

    internal IncomingTransaction(TransactionId id, Amount amount, DateTime transactionDate, Spread? spread = null, Note? note = null) 
        : base(id, amount, transactionDate, spread, note)
    {
    }

    internal IncomingTransaction(TransactionId id, Volume volume, DateTime transactionDate, Note? note) 
        : base(id, volume, transactionDate, note)
    {
    }
}