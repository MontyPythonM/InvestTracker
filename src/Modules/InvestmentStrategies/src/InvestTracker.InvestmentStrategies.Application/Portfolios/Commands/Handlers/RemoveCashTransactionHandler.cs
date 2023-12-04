using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands.Handlers;

internal sealed class RemoveCashTransactionHandler : ICommandHandler<RemoveCashTransaction>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IRequestContext _requestContext;
    
    public RemoveCashTransactionHandler(IPortfolioRepository portfolioRepository, IRequestContext requestContext)
    {
        _portfolioRepository = portfolioRepository;
        _requestContext = requestContext;
    }

    public async Task HandleAsync(RemoveCashTransaction command, CancellationToken token)
    {
        var portfolio = await _portfolioRepository.GetAsync(command.PortfolioId, token);
        if (portfolio is null)
        {
            throw new PortfolioNotFoundException(command.PortfolioId);
        }
        
        var isStakeholderHaveAccess = await _portfolioRepository
            .HasAccessAsync(command.PortfolioId, _requestContext.Identity.UserId, token);
        
        if (isStakeholderHaveAccess is false)
        {
            throw new PortfolioAccessException(command.PortfolioId);
        }
        
        var cash = portfolio.Cash.SingleOrDefault(asset => asset.Id == command.FinancialAssetId);

        if (cash is null)
        {
            throw new FinancialAssetNotFoundException(command.FinancialAssetId);
        }

        cash.RemoveTransaction(command.TransactionId);
        await _portfolioRepository.UpdateAsync(portfolio, token);
    }
}