using InvestTracker.Shared.Abstractions.DDD;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Events;

public record InvestmentStrategyCreated(Guid InvestmentStrategyId) : IDomainEvent;