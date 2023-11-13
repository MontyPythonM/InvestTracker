using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands.Handlers;

internal sealed class AddEdoBondAssetHandler : ICommandHandler<AddEdoBondAsset>
{
    private readonly IInvestmentStrategyRepository _strategyRepository;
    private readonly IRequestContext _requestContext;
    private readonly IEnumerable<IFinancialAssetLimitPolicy> _policies;
    private readonly IPortfolioRepository _portfolioRepository;

    public AddEdoBondAssetHandler(IInvestmentStrategyRepository strategyRepository, IRequestContext requestContext, 
        IEnumerable<IFinancialAssetLimitPolicy> policies, IPortfolioRepository portfolioRepository)
    {
        _strategyRepository = strategyRepository;
        _requestContext = requestContext;
        _policies = policies;
        _portfolioRepository = portfolioRepository;
    }
    
    public async Task HandleAsync(AddEdoBondAsset command, CancellationToken token)
    {
        var currentUserPortfolios = await _strategyRepository.GetOwnerPortfoliosAsync(_requestContext.Identity.UserId, true, token);
        var portfolioId = new PortfolioId(command.PortfolioId);
        
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
        
        portfolio.AddEdoTreasuryBond(Guid.NewGuid(), command.Volume, command.PurchaseDate, 
            command.FirstYearInterestRate, command.Margin, command.Note, assetTypeLimitDto);

        await _portfolioRepository.UpdateAsync(portfolio, token);
    }
}