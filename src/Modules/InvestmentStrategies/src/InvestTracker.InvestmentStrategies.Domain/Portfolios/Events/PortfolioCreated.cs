using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Events;

public record PortfolioCreated(PortfolioId PortfolioId, InvestmentStrategyId InvestmentStrategyId) : IDomainEvent;