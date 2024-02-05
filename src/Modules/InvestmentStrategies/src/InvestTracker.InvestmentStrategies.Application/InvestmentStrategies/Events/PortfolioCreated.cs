using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Events;

public record PortfolioCreated(Guid PortfolioId, string PortfolioTitle, Guid OwnerId, IEnumerable<Guid> CollaboratorIds) : IEvent;