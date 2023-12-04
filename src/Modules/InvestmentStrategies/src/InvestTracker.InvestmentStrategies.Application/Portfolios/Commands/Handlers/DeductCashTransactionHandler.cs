using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands.Handlers;

internal sealed class DeductCashTransactionHandler : ICommandHandler<DeductCashTransaction>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IRequestContext _requestContext;
    private readonly ITimeProvider _timeProvider;

    public DeductCashTransactionHandler(IPortfolioRepository portfolioRepository, IRequestContext requestContext, 
        ITimeProvider timeProvider)
    {
        _portfolioRepository = portfolioRepository;
        _requestContext = requestContext;
        _timeProvider = timeProvider;
    }
    
    public async Task HandleAsync(DeductCashTransaction command, CancellationToken token)
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

        cash.DeductFunds(Guid.NewGuid(), command.Amount, command.TransactionDate, command.Note, _timeProvider.Current());

        await _portfolioRepository.UpdateAsync(portfolio, token);    
    }
}