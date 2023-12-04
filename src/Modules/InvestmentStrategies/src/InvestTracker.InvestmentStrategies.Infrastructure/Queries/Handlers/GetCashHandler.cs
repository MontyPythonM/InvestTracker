using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Amount;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetCashHandler : IQueryHandler<GetCash, CashDto>
{
    private readonly InvestmentStrategiesDbContext _context;
    private readonly IRequestContext _requestContext;
    private readonly ITimeProvider _timeProvider;
    private readonly IPortfolioRepository _portfolioRepository;

    public GetCashHandler(InvestmentStrategiesDbContext context, IRequestContext requestContext, ITimeProvider timeProvider, 
        IPortfolioRepository portfolioRepository)
    {
        _context = context;
        _requestContext = requestContext;
        _timeProvider = timeProvider;
        _portfolioRepository = portfolioRepository;
    }
    
    public async Task<CashDto> HandleAsync(GetCash query, CancellationToken token = default)
    {
        var isStakeholderHaveAccess = await _portfolioRepository
            .HasAccessAsync(query.PortfolioId, _requestContext.Identity.UserId, token);
        
        if (isStakeholderHaveAccess is false)
        {
            throw new PortfolioAccessException(query.PortfolioId);
        }
        
        var cash = await _context.Cash
            .AsNoTracking()
            .Include(asset => asset.Transactions)
            .SingleOrDefaultAsync(asset => asset.Id == query.FinancialAssetId && asset.PortfolioId == query.PortfolioId, token);

        if (cash is null)
        {
            throw new FinancialAssetNotFoundException(query.FinancialAssetId);
        }

        return new CashDto
        {
            Id = cash.Id,
            Currency = cash.Currency,
            Amount = cash.GetCurrentAmount(),
            Note = cash.Note,
            CreatedAt = cash.CreatedAt,
            Transactions = cash.Transactions.OrderBy(t => t.TransactionDate).Select(t => new TransactionDto
            {
                Id = t.Id,
                Date = t.TransactionDate,
                Value = t is IncomingAmountTransaction ? t.Amount : -t.Amount,
                Note = t.Note
            })
        };
    }
}