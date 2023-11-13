using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands.Handlers;

internal sealed class AddCashAssetHandler : ICommandHandler<AddCashAsset>
{
    private readonly IEnumerable<IFinancialAssetLimitPolicy> _policies;
    private readonly IRequestContext _requestContext;
    private readonly IInvestmentStrategyRepository _strategyRepository;
    private readonly ITimeProvider _timeProvider;
    private readonly IPortfolioRepository _portfolioRepository;

    public AddCashAssetHandler(IEnumerable<IFinancialAssetLimitPolicy> policies, IRequestContext requestContext, 
        IInvestmentStrategyRepository strategyRepository, ITimeProvider timeProvider, IPortfolioRepository portfolioRepository)
    {
        _policies = policies;
        _requestContext = requestContext;
        _strategyRepository = strategyRepository;
        _timeProvider = timeProvider;
        _portfolioRepository = portfolioRepository;
    }

    public async Task HandleAsync(AddCashAsset command, CancellationToken token)
    {
        var currentUserPortfolios = await _strategyRepository.GetOwnerPortfoliosAsync(_requestContext.Identity.UserId, true, token);
        var portfolioId = command.PortfolioId;
        
        if (!currentUserPortfolios.Contains(portfolioId))
        {
            throw new IncorrectPortfolioOwnerException(command.PortfolioId);
        }

        var portfolio = await _portfolioRepository.GetAsync(portfolioId, token);
        if (portfolio is null)
        {
            throw new PortfolioNotFoundException(portfolioId);
        }
        
        var assetTypeLimitDto = new AssetLimitPolicyDto(_requestContext.Identity.Subscription, _policies);
        var cash = portfolio.AddCash(Guid.NewGuid(), command.Currency, command.Note, assetTypeLimitDto);
        
        if (command.InitialAmount is not null)
        {
            cash.AddFunds(Guid.NewGuid(), command.InitialAmount, _timeProvider.Current(), $"Opening amount of the cash asset");
        }
        
        await _portfolioRepository.UpdateAsync(portfolio, token);
    }
}