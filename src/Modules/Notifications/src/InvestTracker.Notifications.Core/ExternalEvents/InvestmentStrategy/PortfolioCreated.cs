using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.InvestmentStrategy;

public record PortfolioCreated(Guid PortfolioId, string PortfolioTitle, Guid OwnerId, IEnumerable<Guid> CollaboratorIds) : IEvent;