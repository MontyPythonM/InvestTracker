using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;

public sealed class PortfolioAccessException : InvestTrackerException
{
    public PortfolioAccessException(PortfolioId portfolioId) 
        : base($"Current user does not have access to portfolio with ID: '{portfolioId.Value}' either as an owner or a collaborator")
    {
    }
}