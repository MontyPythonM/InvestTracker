using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Events;

public record AssetInPortfolioAdded(AssetId AssetId, PortfolioId PortfolioId, AssetDataId AssetDataId) : IDomainEvent;