using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands.Handlers;

internal sealed class DeductCashTransactionHandler : ICommandHandler<DeductCashTransaction>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly ITimeProvider _timeProvider;
    private readonly IResourceAccessor _resourceAccessor;
    
    public DeductCashTransactionHandler(IPortfolioRepository portfolioRepository, ITimeProvider timeProvider, 
        IResourceAccessor resourceAccessor)
    {
        _portfolioRepository = portfolioRepository;
        _timeProvider = timeProvider;
        _resourceAccessor = resourceAccessor;
    }
    
    public async Task HandleAsync(DeductCashTransaction command, CancellationToken token)
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

        cash.DeductFunds(Guid.NewGuid(), command.Amount, command.TransactionDate, command.Note, _timeProvider.Current());

        await _portfolioRepository.UpdateAsync(portfolio, token);    
    }
}