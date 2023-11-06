using InvestTracker.InvestmentStrategies.Application.FinancialAssets.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Dto;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Assets;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Policies.AssetLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.InvestmentStrategies.Application.FinancialAssets.Commands.Handlers;

internal sealed class AddCashAssetHandler : ICommandHandler<AddCashAsset>
{
    private readonly IFinancialAssetRepository _assetRepository;
    private readonly IEnumerable<IAssetTypeLimitPolicy> _policies;
    private readonly IRequestContext _requestContext;
    private readonly IInvestmentStrategyRepository _strategyRepository;
    private readonly ITimeProvider _timeProvider;

    public AddCashAssetHandler(IFinancialAssetRepository assetRepository, IEnumerable<IAssetTypeLimitPolicy> policies, 
        IRequestContext requestContext, IInvestmentStrategyRepository strategyRepository, ITimeProvider timeProvider)
    {
        _assetRepository = assetRepository;
        _policies = policies;
        _requestContext = requestContext;
        _strategyRepository = strategyRepository;
        _timeProvider = timeProvider;
    }

    public async Task HandleAsync(AddCashAsset command, CancellationToken token)
    {
        var currentUserPortfolios = await _strategyRepository.GetOwnerPortfolios(_requestContext.Identity.UserId, true, token);
        var portfolioId = new PortfolioId(command.PortfolioId);
        
        if (!currentUserPortfolios.Contains(portfolioId))
        {
            throw new IncorrectPortfolioOwnerException(command.PortfolioId);
        }
        
        var existingPortfolioAssets = await _assetRepository.GetAssetsAsync(portfolioId, token);
        var assetTypeLimitDto = new AssetTypeLimitDto(existingPortfolioAssets.ToHashSet(), 
            _requestContext.Identity.Subscription, _policies);

        var cash = new Cash(Guid.NewGuid(), portfolioId, command.Currency, command.Note, assetTypeLimitDto);
        
        if (command.InitialAmount is not null)
        {
            cash.AddFunds(Guid.NewGuid(), command.InitialAmount, _timeProvider.Current(), $"Opening amount of the cash asset");
        }
        
        await _assetRepository.CreateAsync(cash, token);
    }
}