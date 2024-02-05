using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Events;

public record InvestmentStrategyCreated(Guid InvestmentStrategyId, string InvestmentStrategyTitle, Guid OwnerId) : IEvent;
