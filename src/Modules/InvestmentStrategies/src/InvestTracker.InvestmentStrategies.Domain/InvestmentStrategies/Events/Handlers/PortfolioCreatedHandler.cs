using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Events;
using InvestTracker.Shared.Abstractions.DDD;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Events.Handlers;

internal sealed class PortfolioCreatedHandler : IDomainEventHandler<PortfolioCreated>
{
    private readonly IInvestmentStrategyRepository _strategyRepository;

    public PortfolioCreatedHandler(IInvestmentStrategyRepository strategyRepository)
    {
        _strategyRepository = strategyRepository;
    }

    public async Task HandleAsync(PortfolioCreated @event)
    {
        var strategy = await _strategyRepository.GetAsync(@event.InvestmentStrategyId);

        if (strategy is null)
        {
            return;
        }
        
        strategy.AddPortfolio(@event.PortfolioId);
        await _strategyRepository.UpdateAsync(strategy);
    }
}