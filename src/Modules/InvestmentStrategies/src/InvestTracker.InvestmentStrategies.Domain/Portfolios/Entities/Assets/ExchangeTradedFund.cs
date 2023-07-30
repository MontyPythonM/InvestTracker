using InvestTracker.InvestmentStrategies.Domain.AssetType.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Assets;

public sealed class ExchangeTradedFund : Asset
{
    public Broker? Broker { get; private set; }

    public ExchangeTradedFund(AssetId id, Currency currency, AssetTypeId assetTypeId, Note? note = null) 
        : base(id, currency, assetTypeId, note)
    {
    }
}