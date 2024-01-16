using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Events;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;

public class Portfolio : AggregateRoot<PortfolioId>
{
    private HashSet<Cash> _cash = new();
    private HashSet<EdoTreasuryBond> _edoTreasuryBonds = new();
    private HashSet<CoiTreasuryBond> _coiTreasuryBonds = new();
    
    public Title Title { get; private set; }
    public Note Note { get; private set; }
    public Description Description { get; private set; }
    public InvestmentStrategyId InvestmentStrategyId { get; set; }
    public IEnumerable<Cash> Cash
    {
        get => _cash;
        set => _cash = new HashSet<Cash>(value);
    }
    public IEnumerable<EdoTreasuryBond> EdoTreasuryBonds
    {
        get => _edoTreasuryBonds;
        set => _edoTreasuryBonds = new HashSet<EdoTreasuryBond>(value);
    }
    public IEnumerable<CoiTreasuryBond> CoiTreasuryBonds
    {
        get => _coiTreasuryBonds;
        set => _coiTreasuryBonds = new HashSet<CoiTreasuryBond>(value);
    }

    private Portfolio()
    {
    }

    private Portfolio(PortfolioId id, Title title, Note note, Description description, InvestmentStrategyId investmentStrategyId)
    {
        Id = id;
        Title = title;
        Note = note;
        Description = description;
        InvestmentStrategyId = investmentStrategyId;
        
        AddEvent(new PortfolioCreated(id, investmentStrategyId));
    }

    public static Portfolio Create(PortfolioId id, Title title, Note note, Description description, 
        InvestmentStrategyId investmentStrategyId, PortfolioPolicyLimitDto dto)
    {
        var policy = dto.Policies.SingleOrDefault(policy => policy.CanBeApplied(dto.OwnerSubscription));

        if (policy is null)
        {
            throw new PortfolioLimitPolicyNotFoundException(dto.OwnerSubscription);
        }

        if (!policy.CanAddPortfolio(dto.ExistingPortfolios))
        {
            throw new PortfolioLimitExceedException(dto.OwnerSubscription);
        }

        return new Portfolio(id, title, note, description, investmentStrategyId);
    }

    public EdoTreasuryBond AddEdoTreasuryBond(FinancialAssetId id, Volume volume, DateOnly purchaseDate, 
        InterestRate firstYearInterestRate, Margin margin, Note note, AssetLimitPolicyDto dto)
    {
        var edoBond = new EdoTreasuryBond(id, volume, purchaseDate, firstYearInterestRate, margin, note);

        if (!CanAddFinancialAsset(edoBond, dto))
        {
            throw new AssetLimitExceedException(dto.Subscription);
        }

        _edoTreasuryBonds.Add(edoBond);
        IncrementVersion();

        return edoBond;
    }

    public CoiTreasuryBond AddCoiTreasuryBond(FinancialAssetId id, Volume volume, DateOnly purchaseDate, 
        InterestRate firstYearInterestRate, Margin margin, Note note, AssetLimitPolicyDto dto)
    {
        var coiBond = new CoiTreasuryBond(id, volume, purchaseDate, firstYearInterestRate, margin, note);

        if (!CanAddFinancialAsset(coiBond, dto))
        {
            throw new AssetLimitExceedException(dto.Subscription);
        }

        _coiTreasuryBonds.Add(coiBond);
        IncrementVersion();

        return coiBond;
    }
    
    public Cash AddCash(FinancialAssetId id, Currency currency, Note note, AssetLimitPolicyDto dto)
    {
        var cash = new Cash(id, currency, note);

        if (!CanAddFinancialAsset(cash, dto))
        {
            throw new AssetLimitExceedException(dto.Subscription);
        }

        _cash.Add(cash);
        IncrementVersion();
        
        return cash;
    }
    
    public IEnumerable<IFinancialAsset> GetFinancialAssets()
    {
        var assets = new List<IFinancialAsset>();
        assets.AddRange(_edoTreasuryBonds);
        assets.AddRange(_coiTreasuryBonds);
        assets.AddRange(_cash);

        return assets;
    }
    
    private bool CanAddFinancialAsset(IFinancialAsset asset, AssetLimitPolicyDto dto)
    {
        var policy = dto.Policies.SingleOrDefault(policy => policy.CanBeApplied(dto.Subscription));

        if (policy is null)
        {
            throw new AssetLimitPolicyNotFoundException(dto.Subscription);
        }

        return policy.CanAddAsset(asset, GetFinancialAssets().ToList());
    }
}