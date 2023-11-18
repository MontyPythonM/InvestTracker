using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Events;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.StrategyLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
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
    public ICollection<Guid> Collaborators
    {
        get => _collaborators;
        set => _collaborators = new List<Guid>(value);
    }
    public ICollection<Guid> Portfolios
    {
        get => _portfolios;
        set => _portfolios = new List<Guid>(value);
    }
    
    private List<Guid> _collaborators;
    private List<Guid> _portfolios;

    private InvestmentStrategy()
    {
        _collaborators = new List<Guid>();
        _portfolios = new List<Guid>();
    }

    private InvestmentStrategy(InvestmentStrategyId id, Title title, StakeholderId owner, Note note)
    {
        Id = id;
        Title = title;
        Note = note;
        IsShareEnabled = false;
        Owner = owner;
        _collaborators = new List<Guid>();
        _portfolios = new List<Guid>();
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

    public bool IsOwner(StakeholderId stakeholderId) => Owner == stakeholderId;

    public bool IsCollaborator(StakeholderId stakeholderId)
        => Collaborators.Contains(stakeholderId);
    
    public bool IsStakeholderHaveAccess(StakeholderId stakeholderId)
        => IsOwner(stakeholderId) || (IsCollaborator(stakeholderId) && IsShareEnabled);
    
    internal void AddPortfolio(PortfolioId portfolioId)
    {
        _portfolios.Add(portfolioId);
        IncrementVersion();
    }
    
    internal void RemovePortfolio(PortfolioId portfolioId)
    {
        _portfolios.Remove(portfolioId);
        IncrementVersion();
    }
}