﻿using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Shared;
using InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Messages;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands.Handlers;

internal sealed class AddCoiAssetHandler : ICommandHandler<AddCoiAsset>
{
    private readonly IEnumerable<IFinancialAssetLimitPolicy> _policies;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly IResourceAccessor _resourceAccessor;
    private readonly IMessageBroker _messageBroker;
    private readonly IInvestmentStrategyRepository _strategyRepository;

    public AddCoiAssetHandler(IEnumerable<IFinancialAssetLimitPolicy> policies, IPortfolioRepository portfolioRepository, 
        IStakeholderRepository stakeholderRepository, IResourceAccessor resourceAccessor, IMessageBroker messageBroker, 
        IInvestmentStrategyRepository strategyRepository)
    {
        _policies = policies;
        _portfolioRepository = portfolioRepository;
        _stakeholderRepository = stakeholderRepository;
        _resourceAccessor = resourceAccessor;
        _messageBroker = messageBroker;
        _strategyRepository = strategyRepository;
    }

    public async Task HandleAsync(AddCoiAsset command, CancellationToken token)
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
        
        var coi = portfolio.AddCoiTreasuryBond(Guid.NewGuid(), command.Volume, command.PurchaseDate, 
            command.FirstYearInterestRate, command.Margin, command.Note, assetTypeLimitDto);

        var collaborators = await _strategyRepository.GetCollaboratorsAsync(portfolio.InvestmentStrategyId, token);
        
        await _portfolioRepository.UpdateAsync(portfolio, token);
        await _messageBroker.PublishAsync(new Events.FinancialAssetAdded(coi.Id, coi.AssetName, portfolio.Id, 
            portfolio.Title, owner.Id, collaborators.Select(c => c.Value)));
    }
}