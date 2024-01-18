using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;

public abstract class FinancialAsset
{
    protected HashSet<Transaction> _transactions = new();

    public FinancialAssetId Id { get; protected set; }
    public Currency Currency { get; protected set; }
    public Note Note { get; protected set; }
    public PortfolioId PortfolioId { get; protected set; }
    public abstract string AssetName { get; }
    public IEnumerable<Transaction> Transactions
    {
        get => _transactions;
        set => _transactions = new HashSet<Transaction>(value);
    }
}