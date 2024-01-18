using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Extensions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetCoiBondHandler : IQueryHandler<GetCoiBond, CoiBondDto>
{
    private readonly InvestmentStrategiesDbContext _context;
    private readonly IResourceAccessor _resourceAccessor;
    private readonly ITimeProvider _timeProvider;
    private readonly IInflationRateRepository _inflationRateRepository;

    public GetCoiBondHandler(InvestmentStrategiesDbContext context, IResourceAccessor resourceAccessor, 
        ITimeProvider timeProvider, IInflationRateRepository inflationRateRepository)
    {
        _context = context;
        _resourceAccessor = resourceAccessor;
        _timeProvider = timeProvider;
        _inflationRateRepository = inflationRateRepository;
    }

    public async Task<CoiBondDto> HandleAsync(GetCoiBond query, CancellationToken token = default)
    {
        await _resourceAccessor.CheckAsync(query.PortfolioId, token);
        
        var coi = await _context.CoiTreasuryBonds
            .AsNoTracking()
            .Include(asset => asset.Transactions)
            .SingleOrDefaultAsync(asset => asset.Id == query.FinancialAssetId, token);
        
        if (coi is null)
        {
            throw new FinancialAssetNotFoundException(query.FinancialAssetId);
        }

        var inflationRates = await _inflationRateRepository
            .GetChronologicalRatesAsync(new DateRange(coi.PurchaseDate, coi.GetRedemptionDate()), token);
        
        var now = _timeProvider.CurrentDate();
        var interestRates = coi.CalculateInterestRates(inflationRates, now).ToList();
        
        return new CoiBondDto
        {
            Id = coi.Id,
            Symbol = coi.Symbol,
            Currency = coi.Currency,
            Margin = coi.Margin,
            CurrentCumulativeAmount = coi.GetCumulativeAmount(inflationRates, now),
            CurrentPeriodAmount = coi.GetAmount(inflationRates, now),
            CurrentVolume = coi.GetCurrentVolume(),
            Note = coi.Note,
            PurchaseDate = coi.PurchaseDate,
            RedemptionDate = coi.GetRedemptionDate(),
            CreatedAt = coi.CreatedAt,
            CumulativeInterestRate = interestRates.GetCumulativeInterestRate(),
            PeriodInterestRates = interestRates.Select(rate => (decimal)rate),
            Transactions = coi.Transactions.Select(transaction => new TransactionDto
            {
                Id = transaction.Id,
                Note = transaction.Note,
                Date = transaction.TransactionDate,
                Amount = transaction.Amount
            })
        };
    }
}