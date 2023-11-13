using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Events;

public record FinancialAssetAdded(FinancialAssetId AssetId, PortfolioId PortfolioId) : IDomainEvent;