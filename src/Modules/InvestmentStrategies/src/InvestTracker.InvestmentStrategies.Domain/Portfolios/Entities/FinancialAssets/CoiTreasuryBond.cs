using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Auditable;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;

public class CoiTreasuryBond : IFinancialAsset, IAuditable
{
    private const int NominalUnitValue = 100;
    private const int InvestmentDurationYears = 4;

    public FinancialAssetId Id { get; private set; }
    public Currency Currency { get; private set; }
    public Note Note { get; private set; }
    public PortfolioId PortfolioId { get; private set; }
    public string Symbol { get; private set; }
    public InterestRate FirstYearInterestRate { get; private set; }
    public Margin Margin { get; private set; }
    public DateOnly RedemptionDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedBy { get;  private  set; }
    
    public IEnumerable<VolumeTransaction> Transactions
    {
        get => _transactions;
        set => _transactions = new HashSet<VolumeTransaction>(value);
    }
    
    private HashSet<VolumeTransaction> _transactions = new();
    
    private CoiTreasuryBond()
    {
    }

    public string GetAssetName() => $"{Symbol} Treasury Bond";
}