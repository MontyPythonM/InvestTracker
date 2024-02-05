using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.InvestmentStrategy;

public record InvestmentStrategyShared(Guid InvestmentStrategyId, string InvestmentStrategyTitle, Guid OwnerId, Guid CollaboratorId) : IEvent;