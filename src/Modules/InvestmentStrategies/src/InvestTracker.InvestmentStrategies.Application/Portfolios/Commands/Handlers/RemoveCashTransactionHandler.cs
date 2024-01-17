using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands.Handlers;

internal sealed class RemoveCashTransactionHandler : ICommandHandler<RemoveCashTransaction>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IResourceAccessor _resourceAccessor;
    
    public RemoveCashTransactionHandler(IPortfolioRepository portfolioRepository, IResourceAccessor resourceAccessor)
    {
        _portfolioRepository = portfolioRepository;
        _resourceAccessor = resourceAccessor;
    }

    public async Task HandleAsync(RemoveCashTransaction command, CancellationToken token)
    {
        var portfolio = await _portfolioRepository.GetAsync(command.PortfolioId, token);
        if (portfolio is null)
        {
            throw new PortfolioNotFoundException(command.PortfolioId);
        }

        await _resourceAccessor.CheckAsync(portfolio.Id, token);
        
        var cash = portfolio.FinancialAssets
            .OfType<Cash>()
            .SingleOrDefault(asset => asset.Id == command.FinancialAssetId);

        if (cash is null)
        {
            throw new FinancialAssetNotFoundException(command.FinancialAssetId);
        }

        cash.RemoveTransaction(command.TransactionId);
        await _portfolioRepository.UpdateAsync(portfolio, token);
    }
}