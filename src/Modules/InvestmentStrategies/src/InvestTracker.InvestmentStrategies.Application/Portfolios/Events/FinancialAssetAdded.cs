using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Events;

public record FinancialAssetAdded(Guid FinancialAssetId, string FinancialAssetName, Guid PortfolioId, 
    string PortfolioTitle, Guid OwnerId, IEnumerable<Guid> CollaboratorIds) : IEvent;