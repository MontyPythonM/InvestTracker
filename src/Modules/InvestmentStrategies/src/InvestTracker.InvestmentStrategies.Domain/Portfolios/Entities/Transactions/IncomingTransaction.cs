using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;

public sealed class IncomingTransaction : Transaction
{
    private IncomingTransaction()
    {
    }

    public IncomingTransaction(TransactionId id, ValueObjects.Amount amount, DateTime transactionDate, Note note) 
        : base(id, amount, transactionDate, note)
    {
    }
}