using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Events;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.PortfolioLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.StrategyLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;

public class InvestmentStrategy : AggregateRoot<InvestmentStrategyId>
{
    public Title Title { get; private set; }
    public Note? Note { get; private set; }
    public bool IsShareEnabled { get; private set; }
    public StakeholderId Owner { get; private set; }
    public IEnumerable<StakeholderId> Collaborators => _collaborators;
    public IEnumerable<Portfolio> Portfolios => _portfolios;
    
    private HashSet<StakeholderId> _collaborators = new();
    private HashSet<Portfolio> _portfolios = new();

    private InvestmentStrategy()
    {
    }

    internal InvestmentStrategy(Title title, StakeholderId owner, Note? note = null)
    {
        Title = title;
        Note = note;
        IsShareEnabled = false;
        Owner = owner;
    }

    public static InvestmentStrategy Create(Title title, StakeholderId owner, Note? note, Subscription subscription, 
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
        
        var investmentStrategy = new InvestmentStrategy(title, owner, note);

        investmentStrategy.AddEvent(new InvestmentStrategyCreated());
        return investmentStrategy;
    }

    public void AddPortfolio(Portfolio portfolio, Subscription subscription, IEnumerable<IPortfolioLimitPolicy> policies)
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
        
        _portfolios.Add(portfolio);
    }

    public void AddCollaborator(StakeholderId collaboratorId)
    {
        if (IsShareEnabled is false)
        {
            throw new InvestmentStrategySharedException(Id);
        }

        if (Owner == collaboratorId)
        {
            return;
        }

        _collaborators.Add(collaboratorId);
    }

    public void RemoveCollaborator(StakeholderId collaboratorId)
    {
        _collaborators.Remove(collaboratorId);
    }
}