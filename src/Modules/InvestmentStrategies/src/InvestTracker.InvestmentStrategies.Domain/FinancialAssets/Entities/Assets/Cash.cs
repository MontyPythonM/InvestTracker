using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Dto;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Transactions.Amount;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Assets;

public sealed class Cash : FinancialAsset
{
    public IEnumerable<AmountTransaction> Transactions
    {
        get => _transactions;
        set => _transactions = new HashSet<AmountTransaction>(value);
    }
    
    private HashSet<AmountTransaction> _transactions = new();

    private Cash()
    {
    }
    
    public Cash(FinancialAssetId id, PortfolioId portfolioId, Currency currency, Note note, AssetTypeLimitDto assetTypeLimitDto) 
        : base(id, portfolioId, currency, note, assetTypeLimitDto)
    {
    }
    
    public IncomingAmountTransaction AddFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Note note)
    {
        var transaction = new IncomingAmountTransaction(transactionId, amount, transactionDate, note);
        _transactions.Add(transaction);

        IncrementVersion();
        return transaction;
    }

    public OutgoingAmountTransaction DeductFunds(TransactionId transactionId, Amount amount, DateTime transactionDate, 
        Note note)
    {
        var transaction = new OutgoingAmountTransaction(transactionId, amount, transactionDate, note);
        _transactions.Add(transaction);

        IncrementVersion();
        return transaction;
    }
    
    public Amount GetCurrentAmount() => _transactions.OfType<IncomingAmountTransaction>().Sum(x => x.Amount) - 
                                     _transactions.OfType<OutgoingAmountTransaction>().Sum(x => x.Amount);
}