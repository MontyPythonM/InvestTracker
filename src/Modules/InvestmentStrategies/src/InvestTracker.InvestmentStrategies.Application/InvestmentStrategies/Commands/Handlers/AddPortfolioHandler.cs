using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.PortfolioLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Messages;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands.Handlers;

internal sealed class AddPortfolioHandler : ICommandHandler<AddPortfolio>
{
    private readonly IInvestmentStrategyRepository _investmentStrategyRepository;
    private readonly IEnumerable<IPortfolioLimitPolicy> _policies;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly IResourceAccessor _resourceAccessor;
    private readonly IMessageBroker _messageBroker;
    
    public AddPortfolioHandler(IInvestmentStrategyRepository investmentStrategyRepository, 
        IEnumerable<IPortfolioLimitPolicy> policies, IPortfolioRepository portfolioRepository, 
        IStakeholderRepository stakeholderRepository, IResourceAccessor resourceAccessor, IMessageBroker messageBroker)
    {
        _investmentStrategyRepository = investmentStrategyRepository;
        _policies = policies;
        _portfolioRepository = portfolioRepository;
        _stakeholderRepository = stakeholderRepository;
        _resourceAccessor = resourceAccessor;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(AddPortfolio command, CancellationToken token)
    {
        var strategyId = new InvestmentStrategyId(command.StrategyId);
        
        var strategy = await _investmentStrategyRepository.GetAsync(strategyId, token);
        if (strategy is null)
        {
            throw new InvestmentStrategyNotFoundException(command.StrategyId);
        }

        await _resourceAccessor.CheckAsync(strategyId, token);

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
        await _messageBroker.PublishAsync(new Events.PortfolioCreated(portfolio.Id, portfolio.Title, strategy.Owner, 
            strategy.Collaborators.Select(c => c.CollaboratorId)));
    }
}