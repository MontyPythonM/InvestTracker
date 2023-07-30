using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.AssetType.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Services;

internal interface IAssetPortfolioService
{
    public Entities.Asset CreateAssetInPortfolio(Portfolio portfolio, Subscription subscription, AssetId id, 
        Currency currency, AssetTypeId assetTypeId, PortfolioId portfolioId, Note? note = null);
}