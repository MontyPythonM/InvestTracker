using InvestTracker.InvestmentStrategies.Application.FinancialAssets.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Dto;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Assets;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Policies.AssetLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;

namespace InvestTracker.InvestmentStrategies.Application.FinancialAssets.Commands.Handlers;

internal sealed class AddEdoBondAssetHandler : ICommandHandler<AddEdoBondAsset>
{
    private readonly IInvestmentStrategyRepository _strategyRepository;
    private readonly IRequestContext _requestContext;
    private readonly IFinancialAssetRepository _assetRepository;
    private readonly IEnumerable<IAssetTypeLimitPolicy> _policies;

    public AddEdoBondAssetHandler(IInvestmentStrategyRepository strategyRepository, IRequestContext requestContext, 
        IFinancialAssetRepository assetRepository, IEnumerable<IAssetTypeLimitPolicy> policies)
    {
        _strategyRepository = strategyRepository;
        _requestContext = requestContext;
        _assetRepository = assetRepository;
        _policies = policies;
    }
    
    public async Task HandleAsync(AddEdoBondAsset command, CancellationToken token)
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

        var edoBond = new EdoTreasuryBond(Guid.NewGuid(), portfolioId, command.Volume, command.PurchaseDate, 
            command.FirstYearInterestRate, command.Margin, command.Note, assetTypeLimitDto);
        
        await _assetRepository.CreateAsync(edoBond, token);
    }
}