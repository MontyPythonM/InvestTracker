using InvestTracker.Shared.Abstractions.DDD;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Events;

public record AssetAdded(Guid Id, Guid PortfolioId) : IDomainEvent;