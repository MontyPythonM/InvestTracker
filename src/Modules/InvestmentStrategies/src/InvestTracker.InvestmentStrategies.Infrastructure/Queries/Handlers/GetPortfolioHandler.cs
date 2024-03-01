using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Shared;
using InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetPortfolioHandler : IQueryHandler<GetPortfolio, PortfolioDetailsDto>
{
    private readonly InvestmentStrategiesDbContext _context;
    private readonly ITimeProvider _timeProvider;
    private readonly IInflationRateRepository _inflationRateRepository;
    private readonly IResourceAccessor _resourceAccessor;
    
    public GetPortfolioHandler(InvestmentStrategiesDbContext context, ITimeProvider timeProvider, 
        IInflationRateRepository inflationRateRepository,  IResourceAccessor resourceAccessor)
    {
        _context = context;
        _timeProvider = timeProvider;
        _inflationRateRepository = inflationRateRepository;
        _resourceAccessor = resourceAccessor;
    }
    
    public async Task<PortfolioDetailsDto> HandleAsync(GetPortfolio query, CancellationToken token = default)
    {
        var portfolio = await _context.Portfolios
            .AsNoTracking()
            .ApplyPortfolioIncludes()
            .SingleOrDefaultAsync(portfolio => portfolio.Id == query.PortfolioId, token);

        if (portfolio is null)
        {
            throw new PortfolioNotFoundException(query.PortfolioId);
        }

        await _resourceAccessor.CheckAsync(portfolio.Id, token);

        var inflationRates = await _inflationRateRepository.GetInflationRatesAsync(token);
        var chronologicalInflationRates = new ChronologicalInflationRates(inflationRates);
        
        return new PortfolioDetailsDto
        {
            Id = portfolio.Id,
            Title = portfolio.Title,
            Description = portfolio.Title,
            Note = portfolio.Note,
            FinancialAssets = GetFinancialAssets(portfolio, chronologicalInflationRates)
        };
    }

    private IEnumerable<FinancialAssetDto> GetFinancialAssets(Portfolio portfolio, ChronologicalInflationRates chronologicalInflationRates)
    {
        var financialAssets = new List<FinancialAssetDto>();
        
        financialAssets.AddRange(portfolio.FinancialAssets.OfType<Cash>().Select(asset => new FinancialAssetDto
        {
            Id = asset.Id,
            Name = asset.AssetName,
            Currency = asset.Currency,
            CurrentAmount = asset.GetCurrentAmount()
        }));

        financialAssets.AddRange(portfolio.FinancialAssets.OfType<EdoTreasuryBond>().Select(asset => new FinancialAssetDto
        {
            Id = asset.Id,
            Name = asset.AssetName,
            Currency = asset.Currency,
            CurrentAmount = asset.GetAmount(chronologicalInflationRates, _timeProvider.CurrentDate())
        }));
        
        financialAssets.AddRange(portfolio.FinancialAssets.OfType<CoiTreasuryBond>().Select(asset => new FinancialAssetDto
        {
            Id = asset.Id,
            Name = asset.AssetName,
            Currency = asset.Currency,
            CurrentAmount = asset.GetCumulativeAmount(chronologicalInflationRates, _timeProvider.CurrentDate())
        }));
        
        return financialAssets;
    }
}