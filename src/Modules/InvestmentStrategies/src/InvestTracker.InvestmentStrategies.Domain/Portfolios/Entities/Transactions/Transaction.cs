using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Auditable;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;

public abstract class Transaction : IAuditable
{
    public TransactionId Id { get; }
    public ValueObjects.Amount Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public Note Note { get; set; }
    public DateTime CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedBy { get; private set; }
    
    protected Transaction()
    {
    }
    
    protected Transaction(TransactionId id, ValueObjects.Amount amount, DateTime transactionDate, Note note)
    {
        Id = id;
        Amount = Math.Round(amount, 2);
        TransactionDate = transactionDate;
        Note = note;
    }
}