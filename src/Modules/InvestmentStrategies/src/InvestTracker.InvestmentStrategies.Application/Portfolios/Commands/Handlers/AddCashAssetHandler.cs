using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Shared;
using InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands.Handlers;

internal sealed class AddCashAssetHandler : ICommandHandler<AddCashAsset>
{
    private readonly IEnumerable<IFinancialAssetLimitPolicy> _policies;
    private readonly ITimeProvider _timeProvider;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly IResourceAccessor _resourceAccessor;
    private readonly IMessageBroker _messageBroker;
    private readonly IInvestmentStrategyRepository _strategyRepository;

    public AddCashAssetHandler(IEnumerable<IFinancialAssetLimitPolicy> policies, ITimeProvider timeProvider, 
        IPortfolioRepository portfolioRepository, IStakeholderRepository stakeholderRepository, 
        IResourceAccessor resourceAccessor, IMessageBroker messageBroker, IInvestmentStrategyRepository strategyRepository)
    {
        _policies = policies;
        _timeProvider = timeProvider;
        _portfolioRepository = portfolioRepository;
        _stakeholderRepository = stakeholderRepository;
        _resourceAccessor = resourceAccessor;
        _messageBroker = messageBroker;
        _strategyRepository = strategyRepository;
    }

    public async Task HandleAsync(AddCashAsset command, CancellationToken token)
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
        var cash = portfolio.AddCash(Guid.NewGuid(), command.Currency, command.Note, assetTypeLimitDto);
        
        if (command.InitialAmount is not null && command.InitialDate is not null)
        {
            cash.AddFunds(Guid.NewGuid(), command.InitialAmount, command.InitialDate.Value, 
                $"Opening amount of the cash asset", _timeProvider.Current());
        }
        
        var collaborators = await _strategyRepository.GetCollaboratorsAsync(portfolio.InvestmentStrategyId, token);
        
        await _portfolioRepository.UpdateAsync(portfolio, token);
        await _messageBroker.PublishAsync(new Events.FinancialAssetAdded(cash.Id, cash.AssetName, portfolio.Id, 
            portfolio.Title, owner.Id, collaborators.Select(c => c.Value)));
    }
}