using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

internal class PortfolioNotFoundException : InvestTrackerException
{
    public PortfolioNotFoundException(InvestmentStrategyId investmentStrategyId, PortfolioId portfolioId) 
        : base($"Portfolio with ID: {portfolioId} does not exists in investment strategy with ID: {investmentStrategyId}")
    {
    }
}