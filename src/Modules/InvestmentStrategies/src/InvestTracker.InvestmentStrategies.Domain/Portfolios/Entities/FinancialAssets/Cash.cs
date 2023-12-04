using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Amount;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.Shared.Abstractions.Auditable;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;

public class Cash : IFinancialAsset, IAuditable
{
    public FinancialAssetId Id { get; private set; }
    public Currency Currency { get; private set; }
    public Note Note { get; private set; }
    public PortfolioId PortfolioId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedBy { get; private set; }
    public IEnumerable<AmountTransaction> Transactions
    {
        get => _transactions;
        set => _transactions = new HashSet<AmountTransaction>(value);
    }
    
    private HashSet<AmountTransaction> _transactions = new();

    private Cash()
    {
    }
    
    internal Cash(FinancialAssetId id, Currency currency, Note note)
    {
        Id = id;
        Currency = currency;
        Note = note;
    }
    
    public IncomingAmountTransaction AddFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Note note, DateTime now)
    {
        if (transactionDate > now)
        {
            throw new FutureTransactionException();
        }

        var transaction = new IncomingAmountTransaction(transactionId, amount, transactionDate, note);
        _transactions.Add(transaction);

        return transaction;
    }

    public OutgoingAmountTransaction DeductFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Note note, DateTime now)
    {
        if (transactionDate > now)
        {
            throw new FutureTransactionException();
        }
        
        var transaction = new OutgoingAmountTransaction(transactionId, amount, transactionDate, note);
        _transactions.Add(transaction);

        return transaction;
    }
    
    public decimal GetCurrentAmount() => _transactions.OfType<IncomingAmountTransaction>().Sum(x => x.Amount) - 
                                     _transactions.OfType<OutgoingAmountTransaction>().Sum(x => x.Amount);
    
    public string GetAssetName() => $"Cash ({Currency.Value})";

    public void RemoveTransaction(TransactionId transactionId)
    {
        if (_transactions.All(transaction => transaction.Id != transactionId))
        {
            throw new TransactionsNotFoundException(transactionId);
        }

        _transactions.RemoveWhere(transaction => transaction.Id == transactionId);
    }
}