using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

internal class InvestmentStrategyNotFoundException : InvestTrackerException
{
    public InvestmentStrategyNotFoundException(PortfolioId portfolioId) 
        : base($"Investment strategy that containing a portfolio with ID: {portfolioId} not found.")
    {
    }

    public InvestmentStrategyNotFoundException(InvestmentStrategyId investmentStrategyId) 
        : base($"Investment strategy with ID: {investmentStrategyId} not found.")
    {
    }
}