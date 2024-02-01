using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;

public sealed class StakeholderNotFoundException : InvestTrackerException
{
    public StakeholderNotFoundException(StakeholderId stakeholderId) 
        : base($"Stakeholder with ID: {stakeholderId.Value} not found.")
    {
    }
    
    public StakeholderNotFoundException(PortfolioId portfolioId) 
        : base($"Stakeholder with resource permission to portfolio with ID: {portfolioId} not found.")
    {
    }
}