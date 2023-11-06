using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Events;

public record FinancialAssetAdded(FinancialAssetId AssetId, PortfolioId PortfolioId) : IDomainEvent;