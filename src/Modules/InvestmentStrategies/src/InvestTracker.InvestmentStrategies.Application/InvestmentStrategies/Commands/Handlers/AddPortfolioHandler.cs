using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.PortfolioLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands.Handlers;

internal sealed class AddPortfolioHandler : ICommandHandler<AddPortfolio>
{
    private readonly IInvestmentStrategyRepository _investmentStrategyRepository;
    private readonly IRequestContext _requestContext;
    private readonly IEnumerable<IPortfolioLimitPolicy> _policies;
    private readonly IPortfolioRepository _portfolioRepository;

    public AddPortfolioHandler(IInvestmentStrategyRepository investmentStrategyRepository, IRequestContext requestContext, 
        IEnumerable<IPortfolioLimitPolicy> policies, IPortfolioRepository portfolioRepository)
    {
        _investmentStrategyRepository = investmentStrategyRepository;
        _requestContext = requestContext;
        _policies = policies;
        _portfolioRepository = portfolioRepository;
    }

    public async Task HandleAsync(AddPortfolio command, CancellationToken token)
    {
        var strategyId = new InvestmentStrategyId(command.StrategyId);
        var strategy = await _investmentStrategyRepository.GetAsync(strategyId, token);
        var currentUser = new StakeholderId(_requestContext.Identity.UserId);

        if (strategy is null)
        {
            throw new InvestmentStrategyNotFoundException(command.StrategyId);
        }

        if (!strategy.IsOwner(currentUser))
        {
            throw new IncorrectInvestmentStrategyOwnerException(currentUser);
        }

        var existingPortfolios = await _portfolioRepository.GetByInvestmentStrategyAsync(strategyId, token);
        var portfolioPolicyLimitDto = new PortfolioPolicyLimitDto(existingPortfolios.ToHashSet(), 
            _requestContext.Identity.Subscription, _policies);

        var portfolio = Portfolio.Create(Guid.NewGuid(), command.Title, command.Note, command.Description, 
            strategyId, portfolioPolicyLimitDto);

        await _portfolioRepository.AddAsync(portfolio, token);
    }
}