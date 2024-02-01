using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands.Handlers;

internal sealed class AddEdoBondAssetHandler : ICommandHandler<AddEdoAsset>
{
    private readonly IEnumerable<IFinancialAssetLimitPolicy> _policies;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly IResourceAccessor _resourceAccessor;

    public AddEdoBondAssetHandler(IEnumerable<IFinancialAssetLimitPolicy> policies, IPortfolioRepository portfolioRepository, 
        IStakeholderRepository stakeholderRepository, IResourceAccessor resourceAccessor)
    {
        _policies = policies;
        _portfolioRepository = portfolioRepository;
        _stakeholderRepository = stakeholderRepository;
        _resourceAccessor = resourceAccessor;
    }
    
    public async Task HandleAsync(AddEdoAsset command, CancellationToken token)
    {
        var portfolio = await _portfolioRepository.GetAsync(command.PortfolioId, token);
        if (portfolio is null)
        {
            throw new PortfolioNotFoundException(command.PortfolioId);
        }
        
        await _resourceAccessor.CheckAsync(portfolio.Id, token);

        var owner = await _stakeholderRepository.GetOwnerAsync(portfolio.Id, true, token);
        if (owner is null)
        {
            throw new StakeholderNotFoundException(portfolio.Id);
        }
        
        var assetTypeLimitDto = new AssetLimitPolicyDto(owner.Subscription, _policies);
        
        portfolio.AddEdoTreasuryBond(Guid.NewGuid(), command.Volume, command.PurchaseDate, 
            command.FirstYearInterestRate, command.Margin, command.Note, assetTypeLimitDto);

        await _portfolioRepository.UpdateAsync(portfolio, token);
    }
}