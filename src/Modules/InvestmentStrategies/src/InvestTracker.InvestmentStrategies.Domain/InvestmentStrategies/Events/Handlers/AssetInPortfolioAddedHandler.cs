using InvestTracker.InvestmentStrategies.Domain.Asset.Events;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.Shared.Abstractions.DDD;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Events.Handlers;

internal sealed class AssetInPortfolioAddedHandler : IDomainEventHandler<AssetInPortfolioAdded>
{
    private readonly IInvestmentStrategyRepository _investmentStrategyRepository;

    public AssetInPortfolioAddedHandler(IInvestmentStrategyRepository investmentStrategyRepository)
    {
        _investmentStrategyRepository = investmentStrategyRepository;
    }
    
    public async Task HandleAsync(AssetInPortfolioAdded @event)
    {
        var strategy = await _investmentStrategyRepository.GetAsync(@event.PortfolioId, CancellationToken.None);
        var portfolio = strategy.Portfolios.First(portfolio => portfolio.PortfolioId == @event.PortfolioId);
        
        if (portfolio.Assets.Contains(@event.AssetId))
        {
            return;
        }
        
        portfolio.AddAsset(@event.AssetId);
        await _investmentStrategyRepository.AddAsync(strategy, CancellationToken.None);
    }
}