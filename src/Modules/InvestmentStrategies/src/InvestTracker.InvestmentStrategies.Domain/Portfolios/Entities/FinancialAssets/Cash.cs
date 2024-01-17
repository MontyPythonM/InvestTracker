using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.Shared.Abstractions.Auditable;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;

public class Cash : FinancialAsset, IAuditable
{
    public DateTime CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedBy { get; private set; }

    public override string AssetName => $"Cash ({Currency.Value})";

    private Cash()
    {
    }
    
    internal Cash(FinancialAssetId id, Currency currency, Note note)
    {
        Id = id;
        Currency = currency;
        Note = note;
    }
    
    public IncomingTransaction AddFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Note note, DateTime now)
    {
        if (transactionDate > now)
        {
            throw new FutureTransactionException();
        }

        var transaction = new IncomingTransaction(transactionId, amount, transactionDate, note);
        _transactions.Add(transaction);

        return transaction;
    }

    public OutgoingTransaction DeductFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Note note, DateTime now)
    {
        if (transactionDate > now)
        {
            throw new FutureTransactionException();
        }
        
        var transaction = new OutgoingTransaction(transactionId, amount, transactionDate, note);
        _transactions.Add(transaction);

        return transaction;
    }
    
    public decimal GetCurrentAmount() => _transactions.OfType<IncomingTransaction>().Sum(x => x.Amount) - 
                                         _transactions.OfType<OutgoingTransaction>().Sum(x => x.Amount);

    public decimal GetAmount(DateTime dateTime)
    {
        var existingTransactions = _transactions
            .Where(t => t.TransactionDate <= dateTime)
            .OrderBy(t => t.TransactionDate)
            .ToList();
        
        return existingTransactions.OfType<IncomingTransaction>().Sum(x => x.Amount) - 
               existingTransactions.OfType<OutgoingTransaction>().Sum(x => x.Amount);
    }

    public void RemoveTransaction(TransactionId transactionId)
    {
        if (_transactions.All(transaction => transaction.Id != transactionId))
        {
            throw new TransactionsNotFoundException(transactionId);
        }

        _transactions.RemoveWhere(transaction => transaction.Id == transactionId);
    }
}