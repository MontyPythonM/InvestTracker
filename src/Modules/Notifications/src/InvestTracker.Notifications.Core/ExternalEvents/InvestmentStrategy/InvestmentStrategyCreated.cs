using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.InvestmentStrategy;

public record InvestmentStrategyCreated(Guid InvestmentStrategyId, string InvestmentStrategyTitle, Guid OwnerId) : IEvent;
