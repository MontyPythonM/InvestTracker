using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.SharedExceptions;

public sealed class IncorrectPortfolioOwnerException : InvestTrackerException
{
    public IncorrectPortfolioOwnerException(PortfolioId portfolioId) : base($"Current user is not the owner of portfolio with ID: {portfolioId}")
    {
    }
}