using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Application.FinancialAssets.Exceptions;

internal class IncorrectPortfolioOwnerException : InvestTrackerException
{
    public IncorrectPortfolioOwnerException(PortfolioId portfolioId) : base($"Current user is not the owner of portfolio with ID: {portfolioId}")
    {
    }
}