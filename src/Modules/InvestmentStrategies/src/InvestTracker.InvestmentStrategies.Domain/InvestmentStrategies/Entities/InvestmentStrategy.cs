using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
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
    
    private HashSet<Portfolio> _portfolios = new();
    private HashSet<StakeholderId> _collaborators = new();

    private InvestmentStrategy()
    {
    }

    public InvestmentStrategy(Title title, StakeholderId owner, Note? note = null)
    {
        Title = title;
        Note = note;
        IsShareEnabled = false;
        Owner = owner;
    }

    internal void AddPortfolio(PortfolioId portfolioId, Title title, Note? note = null, Description? description = null)
    {
        var portfolio = new Portfolio(portfolioId, title, note, description);
        _portfolios.Add(portfolio);
    }

    internal void AddCollaborator(StakeholderId collaboratorId)
    {
        if (IsShareEnabled is false)
        {
            throw new InvestmentStrategySharedException(Id);
        }

        _collaborators.Add(collaboratorId);
    }

    internal void RemoveCollaborator(StakeholderId collaboratorId)
    {
        _collaborators.Remove(collaboratorId);
    }
}