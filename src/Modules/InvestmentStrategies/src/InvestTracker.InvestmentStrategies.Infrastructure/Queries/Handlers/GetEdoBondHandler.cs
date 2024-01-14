using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Extensions;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetEdoBondHandler : IQueryHandler<GetEdoBond, EdoBondDto>
{
    private readonly IResourceAccessor _resourceAccessor;
    private readonly InvestmentStrategiesDbContext _context;
    private readonly ITimeProvider _timeProvider;
    private readonly IInflationRateRepository _inflationRateRepository;
    
    public GetEdoBondHandler(IResourceAccessor resourceAccessor, InvestmentStrategiesDbContext context, 
    ITimeProvider timeProvider, IInflationRateRepository inflationRateRepository)
    {
        _resourceAccessor = resourceAccessor;
        _context = context;
        _timeProvider = timeProvider;
        _inflationRateRepository = inflationRateRepository;
    }

    public async Task<EdoBondDto> HandleAsync(GetEdoBond query, CancellationToken token = default)
    {
       await _resourceAccessor.CheckAsync(query.PortfolioId, token);

       var edo = await _context.EdoTreasuryBonds
           .AsNoTracking()
           .Include(bond => bond.Transactions)
           .SingleOrDefaultAsync(bond => bond.Id == query.FinancialAssetId, token);

       if (edo is null)
       {
           throw new FinancialAssetNotFoundException(query.FinancialAssetId);
       }

       var now = _timeProvider.CurrentDate();
       var inflationRates = await _inflationRateRepository
           .GetChronologicalInflationRatesAsync(new DateRange(edo.PurchaseDate, now), token);

       var interestRates = edo.CalculateInterestRates(inflationRates, now).ToList();

       return new EdoBondDto
       {
           Id = edo.Id,
           Symbol = edo.Symbol,
           Currency = edo.Currency,
           Margin = edo.Margin,
           CurrentAmount = edo.GetCurrentAmount(inflationRates, now),
           CurrentVolume = edo.GetCurrentVolume(),
           Note = edo.Note,
           PurchaseDate = edo.PurchaseDate,
           RedemptionDate = edo.GetRedemptionDate(),
           CreatedAt = edo.CreatedAt,
           CumulativeInterestRate = interestRates.GetCumulativeInterestRate(),
           PeriodInterestRates = interestRates.Select(rate => (decimal)rate),
           Transactions = edo.Transactions.Select(transaction => new TransactionDto
           {
               Id = transaction.Id,
               Note = transaction.Note,
               Date = transaction.TransactionDate,
               Value = transaction.Volume 
           })
       };
    }
}