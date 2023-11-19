using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Amount;

public sealed class IncomingAmountTransaction : AmountTransaction
{
    private IncomingAmountTransaction()
    {
    }

    public IncomingAmountTransaction(TransactionId id, ValueObjects.Amount amount, DateTime transactionDate, Note note) 
        : base(id, amount, transactionDate, note)
    {
    }
}