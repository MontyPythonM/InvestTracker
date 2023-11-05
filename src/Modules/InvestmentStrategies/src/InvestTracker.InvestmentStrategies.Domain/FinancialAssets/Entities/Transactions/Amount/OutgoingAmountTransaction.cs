using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Transactions.Amount;

public sealed class OutgoingAmountTransaction : AmountTransaction
{
    private OutgoingAmountTransaction()
    {
    }
    
    public OutgoingAmountTransaction(TransactionId id, ValueObjects.Amount amount, DateTime transactionDate, Note note) 
        : base(id, amount, transactionDate, note)
    {
    }
}