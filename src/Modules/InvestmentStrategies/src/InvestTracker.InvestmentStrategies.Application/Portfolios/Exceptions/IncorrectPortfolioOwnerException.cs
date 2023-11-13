using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Exceptions;

internal class IncorrectPortfolioOwnerException : InvestTrackerException
{
    public IncorrectPortfolioOwnerException(PortfolioId portfolioId) : base($"Current user is not the owner of portfolio with ID: {portfolioId}")
    {
    }
}