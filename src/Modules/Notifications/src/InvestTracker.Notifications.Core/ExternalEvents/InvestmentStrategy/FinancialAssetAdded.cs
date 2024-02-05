using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.ExternalEvents.InvestmentStrategy;

public record FinancialAssetAdded(Guid FinancialAssetId, string FinancialAssetName, Guid PortfolioId, 
    string PortfolioTitle, Guid OwnerId, IEnumerable<Guid> CollaboratorIds) : IEvent;