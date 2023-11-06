using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Events;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.Shared.Abstractions.DDD;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Events.Handlers;

internal sealed class FinancialAssetAddedHandler : IDomainEventHandler<FinancialAssetAdded>
{
    private readonly IInvestmentStrategyRepository _investmentStrategyRepository;

    public FinancialAssetAddedHandler(IInvestmentStrategyRepository investmentStrategyRepository)
    {
        _investmentStrategyRepository = investmentStrategyRepository;
    }

    public async Task HandleAsync(FinancialAssetAdded @event)
    {
        var strategy = await _investmentStrategyRepository.GetByPortfolioAsync(@event.PortfolioId);
        if (strategy is null)
        {
            return;
        }

        strategy.AddFinancialAsset(@event.PortfolioId, @event.AssetId);

        await _investmentStrategyRepository.UpdateAsync(strategy);
    }
}