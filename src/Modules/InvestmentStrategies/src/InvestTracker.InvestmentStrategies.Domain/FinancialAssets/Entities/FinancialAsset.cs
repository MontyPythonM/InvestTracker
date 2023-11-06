using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Dto;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Events;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities;

public abstract class FinancialAsset : AggregateRoot<FinancialAssetId>
{
    public PortfolioId PortfolioId { get; private set; }
    public Currency Currency { get; private set; }
    public Note Note { get; private set; }
    
    protected FinancialAsset()
    {
    }
    
    protected FinancialAsset(FinancialAssetId id, PortfolioId portfolioId, Currency currency, Note note, AssetTypeLimitDto dto)
    {
        var policy = dto.Policies.SingleOrDefault(policy => policy.CanBeApplied(dto.Subscription));

        if (policy is null)
        {
            throw new AssetLimitPolicyNotFoundException(dto.Subscription);
        }

        if (!policy.CanAddAssetType(dto.ExistingPortfolioAssets))
        {
            throw new AssetTypeLimitExceedException(dto.Subscription);
        }
        
        Id = id;
        Currency = currency;
        PortfolioId = portfolioId;
        Note = note;
        
        AddEvent(new FinancialAssetAdded(id, portfolioId));
    }
    
    public abstract string GetAssetName();
}