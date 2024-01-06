using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands.Handlers;

internal sealed class CreateEdoBondAssetHandler : ICommandHandler<CrateEdoBondAsset>
{
    private readonly IInvestmentStrategyRepository _strategyRepository;
    private readonly IEnumerable<IFinancialAssetLimitPolicy> _policies;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly IResourceAccessor _resourceAccessor;

    public CreateEdoBondAssetHandler(IInvestmentStrategyRepository strategyRepository, 
        IEnumerable<IFinancialAssetLimitPolicy> policies, IPortfolioRepository portfolioRepository, 
        IStakeholderRepository stakeholderRepository, IResourceAccessor resourceAccessor)
    {
        _strategyRepository = strategyRepository;
        _policies = policies;
        _portfolioRepository = portfolioRepository;
        _stakeholderRepository = stakeholderRepository;
        _resourceAccessor = resourceAccessor;
    }
    
    public async Task HandleAsync(CrateEdoBondAsset command, CancellationToken token)
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
        
        portfolio.AddEdoTreasuryBond(Guid.NewGuid(), command.Volume, command.PurchaseDate, 
            command.FirstYearInterestRate, command.Margin, command.Note, assetTypeLimitDto);

        await _portfolioRepository.UpdateAsync(portfolio, token);
    }
}