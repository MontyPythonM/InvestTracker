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
    public bool IsLocked { get; private set; }
    public StakeholderId Owner { get; private set; }
    public ICollection<RelatedCollaborators> Collaborators { get; set; } = new List<RelatedCollaborators>();
    public ICollection<RelatedPortfolios> Portfolios { get; set; } = new List<RelatedPortfolios>();
    
    private InvestmentStrategy()
    {
    }

    private InvestmentStrategy(InvestmentStrategyId id, Title title, StakeholderId owner, Note note)
    {
        Id = id;
        Title = title;
        Note = note;
        IsShareEnabled = false;
        IsLocked = false;
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

        return investmentStrategy;
    }

    public void AssignCollaborator(StakeholderId advisorId, StakeholderId principalId)
    {
        if (IsLocked)
        {
            throw new InvestmentStrategyLockedException(Id);
        }
        
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

        Collaborators.Add(new RelatedCollaborators(advisorId));
        IncrementVersion();
    }

    public void RemoveCollaborator(StakeholderId advisorId, StakeholderId principalId)
    {
        if (Owner.Value != principalId.Value)
        {
            throw new OwnerIsNotPrincipalOfCollaborationException(Id);
        }
        
        Collaborators.Remove(new RelatedCollaborators(advisorId));
        IncrementVersion();
    }

    public bool IsStakeholderHaveAccess(StakeholderId stakeholderId)
        => !IsLocked && (IsOwner(stakeholderId) || (IsCollaborator(stakeholderId) && IsShareEnabled));
    
    internal bool IsOwner(StakeholderId stakeholderId) => Owner == stakeholderId;

    internal bool IsCollaborator(StakeholderId stakeholderId)
        => Collaborators.Select(c => c.CollaboratorId).Contains(stakeholderId.Value);
    
    internal void AddPortfolio(PortfolioId portfolioId)
    {
        if (IsLocked)
        {
            throw new InvestmentStrategyLockedException(Id);
        }
        
        Portfolios.Add(new RelatedPortfolios(portfolioId));
        IncrementVersion();
    }
    
    internal void RemovePortfolio(PortfolioId portfolioId)
    {
        if (IsLocked)
        {
            throw new InvestmentStrategyLockedException(Id);
        }
        
        Portfolios.Remove(new RelatedPortfolios(portfolioId));
        IncrementVersion();
    }

    public void Lock()
    {
        IsLocked = true;
        IsShareEnabled = false;
        IncrementVersion();
    }

    public void Unlock()
    {
        IsLocked = false;
        IncrementVersion();
    }
}