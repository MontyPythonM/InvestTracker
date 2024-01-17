using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;

public sealed class OutgoingTransaction : Transaction
{
    private OutgoingTransaction()
    {
    }
    
    public OutgoingTransaction(TransactionId id, ValueObjects.Amount amount, DateTime transactionDate, Note note) 
        : base(id, amount, transactionDate, note)
    {
    }
}