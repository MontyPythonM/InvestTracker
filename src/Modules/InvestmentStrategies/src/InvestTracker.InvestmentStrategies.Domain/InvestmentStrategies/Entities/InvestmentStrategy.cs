using InvestTracker.InvestmentStrategies.Domain.Collaborations.ValueObjects.Types;
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
    public IEnumerable<StakeholderId> Collaborators => _collaborators;
    public IEnumerable<Portfolio> Portfolios => _portfolios;
    
    private HashSet<StakeholderId> _collaborators = new();
    private HashSet<Portfolio> _portfolios = new();

    private InvestmentStrategy()
    {
    }

    private InvestmentStrategy(InvestmentStrategyId id, Title title, StakeholderId owner, Note note)
    {
        Id = id;
        Title = title;
        Note = note;
        IsShareEnabled = false;
        Owner = owner;
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

    public Portfolio AddPortfolio(PortfolioId id, Title title, Note note, Description description, 
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

        return portfolio;
    }

    public void AssignCollaborator(CollaborationId collaborationId)
    {
        if (IsShareEnabled is false)
        {
            throw new InvestmentStrategySharedException(Id);
        }

        if (Owner.Value != collaborationId.PrincipalId)
        {
            throw new OwnerIsNotPrincipalOfCollaborationException(Id);
        }

        if (Owner.Value == collaborationId.AdvisorId)
        {
            return;
        }

        _collaborators.Add(collaborationId.AdvisorId);
    }

    public void RemoveCollaborator(CollaborationId collaborationId)
    {
        if (Owner.Value != collaborationId.PrincipalId)
        {
            throw new OwnerIsNotPrincipalOfCollaborationException(Id);
        }
        
        _collaborators.Remove(collaborationId.AdvisorId);
    }
}