using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.InvestmentStrategies.Infrastructure.Services.Charts;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetCashChartHandler : IQueryHandler<GetCashChart, CashChart>
{
    private readonly InvestmentStrategiesDbContext _context;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IChartService _chartService;
    private readonly IResourceAccessor _resourceAccessor;
    
    public GetCashChartHandler(InvestmentStrategiesDbContext context, IExchangeRateRepository exchangeRateRepository,
        IChartService chartService, IResourceAccessor resourceAccessor)
    {
        _context = context;
        _exchangeRateRepository = exchangeRateRepository;
        _chartService = chartService;
        _resourceAccessor = resourceAccessor;
    }

    public async Task<CashChart> HandleAsync(GetCashChart query, CancellationToken token = default)
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
        
        var exchangeRates = await _exchangeRateRepository.GetAsync(cash.Currency, query.DisplayInCurrency, query.DateRange, token);

        return _chartService.CalculateCashChart(exchangeRates, cash.Transactions);
    }
}