using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.PortfolioLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
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
    private readonly IStakeholderRepository _stakeholderRepository;

    public AddPortfolioHandler(IInvestmentStrategyRepository investmentStrategyRepository, IRequestContext requestContext, 
        IEnumerable<IPortfolioLimitPolicy> policies, IPortfolioRepository portfolioRepository, IStakeholderRepository stakeholderRepository)
    {
        _investmentStrategyRepository = investmentStrategyRepository;
        _requestContext = requestContext;
        _policies = policies;
        _portfolioRepository = portfolioRepository;
        _stakeholderRepository = stakeholderRepository;
    }

    public async Task HandleAsync(AddPortfolio command, CancellationToken token)
    {
        var strategyId = new InvestmentStrategyId(command.StrategyId);
        var currentUser = new StakeholderId(_requestContext.Identity.UserId);
        
        var strategy = await _investmentStrategyRepository.GetAsync(strategyId, token);
        if (strategy is null)
        {
            throw new InvestmentStrategyNotFoundException(command.StrategyId);
        }

        if (!strategy.IsStakeholderHaveAccess(currentUser))
        {
            throw new InvestmentStrategyAccessException(strategyId);
        }

        var ownerSubscription = await _stakeholderRepository.GetSubscriptionAsync(strategy.Owner, token);
        if (ownerSubscription is null)
        {
            throw new StakeholderNotFoundException(strategy.Owner);
        }

        var existingPortfolios = await _portfolioRepository.GetByInvestmentStrategyAsync(strategyId, true, token);
        var portfolioPolicyLimitDto = new PortfolioPolicyLimitDto(existingPortfolios.ToHashSet(), 
            ownerSubscription, _policies);

        var portfolio = Portfolio.Create(Guid.NewGuid(), command.Title, command.Note, command.Description, 
            strategyId, portfolioPolicyLimitDto);

        await _portfolioRepository.AddAsync(portfolio, token);
    }
}