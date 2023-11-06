using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Events;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.PortfolioLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.StrategyLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;

public class InvestmentStrategy : AggregateRoot<InvestmentStrategyId>
{
    public Title Title { get; private set; }
    public Note Note { get; private set; }
    public bool IsShareEnabled { get; private set; }
    public StakeholderId Owner { get; private set; }
    public IEnumerable<StakeholderId> Collaborators
    {
        get => _collaborators;
        set => _collaborators = new HashSet<StakeholderId>(value);
    }
    public IEnumerable<Portfolio> Portfolios
    {
        get => _portfolios;
        set => _portfolios = new HashSet<Portfolio>(value);
    }
    
    private HashSet<StakeholderId> _collaborators;
    private HashSet<Portfolio> _portfolios;

    private InvestmentStrategy()
    {
        _collaborators = new HashSet<StakeholderId>();
        _portfolios = new HashSet<Portfolio>();
    }

    private InvestmentStrategy(InvestmentStrategyId id, Title title, StakeholderId owner, Note note)
    {
        Id = id;
        Title = title;
        Note = note;
        IsShareEnabled = false;
        Owner = owner;
        _collaborators = new HashSet<StakeholderId>();
        _portfolios = new HashSet<Portfolio>();
    }

    public static InvestmentStrategy Create(Title title, StakeholderId owner, Note note, Subscription subscription, 
        IEnumerable<IStrategyLimitPolicy> policies, IEnumerable<InvestmentStrategy> existingOwnerStrategies)
    {
        var policy = policies.SingleOrDefault(policy => policy.CanBeApplied(subscription));

        if (policy is null)
        {
            throw new StrategyLimitPolicyNotFoundException(subscription);
        }

        if (!policy.CanAddInvestmentStrategy(owner, existingOwnerStrategies))
        {
            throw new StrategyLimitExceededException(subscription);
        }

        var strategyId = new InvestmentStrategyId(Guid.NewGuid());
        var investmentStrategy = new InvestmentStrategy(strategyId, title, owner, note);

        investmentStrategy.AddEvent(new InvestmentStrategyCreated(strategyId));
        return investmentStrategy;
    }

    public void AddPortfolio(PortfolioId id, Title title, Note note, Description description, 
        Subscription subscription, IEnumerable<IPortfolioLimitPolicy> policies)
    {
        var policy = policies.SingleOrDefault(policy => policy.CanBeApplied(subscription));

        if (policy is null)
        {
            throw new PortfolioLimitPolicyNotFoundException(subscription);
        }

        if (!policy.CanAddPortfolio(_portfolios))
        {
            throw new PortfolioLimitExceedException(subscription);
        }

        var portfolio = new Portfolio(id, title, note, description);
        _portfolios.Add(portfolio);
        IncrementVersion();
    }

    public void AssignCollaborator(StakeholderId advisorId, StakeholderId principalId)
    {
        if (IsShareEnabled is false)
        {
            throw new InvestmentStrategySharedException(Id);
        }

        if (Owner.Value != principalId.Value)
        {
            throw new OwnerIsNotPrincipalOfCollaborationException(Id);
        }

        if (Owner.Value == advisorId.Value)
        {
            return;
        }

        _collaborators.Add(advisorId);
        IncrementVersion();
    }

    public void RemoveCollaborator(StakeholderId advisorId, StakeholderId principalId)
    {
        if (Owner.Value != principalId.Value)
        {
            throw new OwnerIsNotPrincipalOfCollaborationException(Id);
        }
        
        _collaborators.Remove(advisorId);
        IncrementVersion();
    }

    public bool IsOwner(Guid userId) => Owner.Value == userId;
    
    internal void AddFinancialAsset(PortfolioId portfolioId, FinancialAssetId assetId)
    {
        var portfolio = Portfolios.FirstOrDefault(portfolio => portfolio.Id == portfolioId);
        if (portfolio is null)
        {
            throw new PortfolioNotFoundException(Id, portfolioId);
        }

        portfolio.AddFinancialAsset(assetId);
        IncrementVersion();
    }

    internal void RemoveFinancialAsset(PortfolioId portfolioId, FinancialAssetId assetId)
    {
        var portfolio = Portfolios.FirstOrDefault(portfolio => portfolio.Id == portfolioId);
        if (portfolio is null)
        {
            throw new PortfolioNotFoundException(Id, portfolioId);
        }
        
        portfolio.RemoveFinancialAsset(assetId);
        IncrementVersion();
    }
}