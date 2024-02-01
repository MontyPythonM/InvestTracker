using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;

public sealed class InvestmentStrategyNotFoundException : InvestTrackerException
{
    public InvestmentStrategyNotFoundException(PortfolioId portfolioId) 
        : base($"Investment strategy that containing a portfolio with ID: '{portfolioId.Value}' not found.")
    {
    }

    public InvestmentStrategyNotFoundException(InvestmentStrategyId id) : base($"Investment strategy with ID: '{id.Value}' not found.")
    {
    }
}