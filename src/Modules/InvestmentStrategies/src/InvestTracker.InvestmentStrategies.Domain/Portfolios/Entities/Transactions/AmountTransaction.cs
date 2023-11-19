using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;

public abstract class AmountTransaction
{
    public TransactionId Id { get; }
    public ValueObjects.Amount Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public Note Note { get; set; }

    protected AmountTransaction()
    {
    }
    
    protected AmountTransaction(TransactionId id, ValueObjects.Amount amount, DateTime transactionDate, Note note)
    {
        Id = id;
        Amount = amount;
        TransactionDate = transactionDate;
        Note = note;
    }
}