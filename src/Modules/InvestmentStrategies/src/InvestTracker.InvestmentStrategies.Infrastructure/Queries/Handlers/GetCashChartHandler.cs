using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.InvestmentStrategies.Infrastructure.Services.Charts;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetCashChartHandler : IQueryHandler<GetCashChart, IEnumerable<CashChartValue>>
{
    private readonly InvestmentStrategiesDbContext _context;
    private readonly IRequestContext _requestContext;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IChartService _chartService;
    
    public GetCashChartHandler(InvestmentStrategiesDbContext context, IRequestContext requestContext, 
        IExchangeRateRepository exchangeRateRepository, IPortfolioRepository portfolioRepository, IChartService chartService)
    {
        _context = context;
        _requestContext = requestContext;
        _exchangeRateRepository = exchangeRateRepository;
        _portfolioRepository = portfolioRepository;
        _chartService = chartService;
    }

    public async Task<IEnumerable<CashChartValue>> HandleAsync(GetCashChart query, CancellationToken token = default)
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
            .Where(asset => asset.Id == query.FinancialAssetId && asset.PortfolioId == query.PortfolioId)
            .SingleOrDefaultAsync(token);

        if (cash is null)
        {
            throw new FinancialAssetNotFoundException(query.FinancialAssetId);
        }
        
        var exchangeRates = await _exchangeRateRepository.GetAsync(cash.Currency, query.DisplayInCurrency, query.DateRange, token);

        return _chartService.CalculateCashChart(exchangeRates, cash.Transactions, cash.Currency, query.DisplayInCurrency);
    }
}