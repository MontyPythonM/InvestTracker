using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.SharedExceptions;

public sealed class PortfolioNotFoundException : InvestTrackerException
{
    public PortfolioNotFoundException(PortfolioId portfolioId) : base($"Portfolio with ID: {portfolioId} does not exists")
    {
    }
}