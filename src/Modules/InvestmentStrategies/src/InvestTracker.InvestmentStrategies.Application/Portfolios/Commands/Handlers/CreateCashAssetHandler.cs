using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands.Handlers;

internal sealed class CreateCashAssetHandler : ICommandHandler<CreateCashAsset>
{
    private readonly IEnumerable<IFinancialAssetLimitPolicy> _policies;
    private readonly IInvestmentStrategyRepository _strategyRepository;
    private readonly ITimeProvider _timeProvider;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly IResourceAccessor _resourceAccessor;
    
    public CreateCashAssetHandler(IEnumerable<IFinancialAssetLimitPolicy> policies, 
        IInvestmentStrategyRepository strategyRepository, ITimeProvider timeProvider, 
        IPortfolioRepository portfolioRepository, IStakeholderRepository stakeholderRepository, 
        IResourceAccessor resourceAccessor)
    {
        _policies = policies;
        _strategyRepository = strategyRepository;
        _timeProvider = timeProvider;
        _portfolioRepository = portfolioRepository;
        _stakeholderRepository = stakeholderRepository;
        _resourceAccessor = resourceAccessor;
    }

    public async Task HandleAsync(CreateCashAsset command, CancellationToken token)
    {
        var portfolioId = new PortfolioId(command.PortfolioId);
        var strategy = await _strategyRepository.GetByPortfolioAsync(portfolioId, true, token);

        if (strategy is null)
        {
            throw new InvestmentStrategyNotFoundException(portfolioId);
        }

        _resourceAccessor.Check(strategy);

        var portfolio = await _portfolioRepository.GetAsync(portfolioId, token);
        if (portfolio is null)
        {
            throw new PortfolioNotFoundException(portfolioId);
        }
        
        var ownerSubscription = await _stakeholderRepository.GetSubscriptionAsync(strategy.Owner, token);
        if (ownerSubscription is null)
        {
            throw new StakeholderNotFoundException(strategy.Owner);
        }
        
        var assetTypeLimitDto = new AssetLimitPolicyDto(ownerSubscription, _policies);
        var cash = portfolio.AddCash(Guid.NewGuid(), command.Currency, command.Note, assetTypeLimitDto);
        
        if (command.InitialAmount is not null && command.InitialDate is not null)
        {
            cash.AddFunds(Guid.NewGuid(), command.InitialAmount, command.InitialDate.Value, 
                $"Opening amount of the cash asset", _timeProvider.Current());
        }
        
        await _portfolioRepository.UpdateAsync(portfolio, token);
    }
}