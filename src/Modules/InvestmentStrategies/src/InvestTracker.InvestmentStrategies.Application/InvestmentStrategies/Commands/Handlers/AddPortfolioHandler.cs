using InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.PortfolioLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands.Handlers;

internal sealed class AddPortfolioHandler : ICommandHandler<AddPortfolio>
{
    private readonly IInvestmentStrategyRepository _investmentStrategyRepository;
    private readonly IRequestContext _requestContext;
    private readonly IEnumerable<IPortfolioLimitPolicy> _policies;

    public AddPortfolioHandler(IInvestmentStrategyRepository investmentStrategyRepository, IRequestContext requestContext, 
        IEnumerable<IPortfolioLimitPolicy> policies)
    {
        _investmentStrategyRepository = investmentStrategyRepository;
        _requestContext = requestContext;
        _policies = policies;
    }

    public async Task HandleAsync(AddPortfolio command, CancellationToken token)
    {
        var strategy = await _investmentStrategyRepository.GetAsync(command.StrategyId, token);
        
        if (strategy is null)
        {
            throw new InvestmentStrategyNotFoundException(command.StrategyId);
        }

        if (!strategy.IsOwner(_requestContext.Identity.UserId))
        {
            throw new IncorrectInvestmentStrategyOwnerException(strategy.Id);
        }

        strategy.AddPortfolio(Guid.NewGuid(), command.Title, command.Note, command.Description, 
            _requestContext.Identity.Subscription, _policies);
        
        await _investmentStrategyRepository.UpdateAsync(strategy, token);
    }
}