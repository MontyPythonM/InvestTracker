using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetCashHandler : IQueryHandler<GetCash, CashDto>
{
    private readonly InvestmentStrategiesDbContext _context;
    private readonly IResourceAccessor _resourceAccessor;
    
    public GetCashHandler(InvestmentStrategiesDbContext context, IResourceAccessor resourceAccessor)
    {
        _context = context;
        _resourceAccessor = resourceAccessor;
    }
    
    public async Task<CashDto> HandleAsync(GetCash query, CancellationToken token = default)
    {
        await _resourceAccessor.CheckAsync(query.PortfolioId, token);
        
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
                Amount = t is IncomingTransaction ? t.Amount : -t.Amount,
                Note = t.Note
            })
        };
    }
}