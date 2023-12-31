﻿using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetPortfolioHandler : IQueryHandler<GetPortfolio, PortfolioDetailsDto>
{
    private readonly InvestmentStrategiesDbContext _context;
    private readonly IRequestContext _requestContext;
    private readonly ITimeProvider _timeProvider;
    private readonly IInflationRateRepository _inflationRateRepository;
    private readonly IPortfolioRepository _portfolioRepository;

    public GetPortfolioHandler(InvestmentStrategiesDbContext context, IRequestContext requestContext, 
        ITimeProvider timeProvider, IInflationRateRepository inflationRateRepository, IPortfolioRepository portfolioRepository)
    {
        _context = context;
        _requestContext = requestContext;
        _timeProvider = timeProvider;
        _inflationRateRepository = inflationRateRepository;
        _portfolioRepository = portfolioRepository;
    }
    
    public async Task<PortfolioDetailsDto> HandleAsync(GetPortfolio query, CancellationToken token = default)
    {
        var portfolio = await _context.Portfolios
            .AsNoTracking()
            .IncludeAssetsAndTransactions()
            .SingleOrDefaultAsync(portfolio => portfolio.Id == query.PortfolioId, token);

        if (portfolio is null)
        {
            throw new PortfolioNotFoundException(query.PortfolioId);
        }

        var isStakeholderHaveAccess = await _portfolioRepository
            .HasAccessAsync(portfolio.Id, _requestContext.Identity.UserId, token);
        
        if (isStakeholderHaveAccess is false)
        {
            throw new PortfolioAccessException(portfolio.Id);
        }

        var inflationRates = await _inflationRateRepository.GetInflationRates(token);
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
        
        financialAssets.AddRange(portfolio.Cash.Select(asset => new FinancialAssetDto
        {
            Id = asset.Id,
            Name = asset.GetAssetName(),
            Currency = asset.Currency,
            CurrentAmount = asset.GetCurrentAmount()
        }));

        financialAssets.AddRange(portfolio.EdoTreasuryBonds.Select(asset => new FinancialAssetDto
        {
            Id = asset.Id,
            Name = asset.GetAssetName(),
            Currency = asset.Currency,
            CurrentAmount = asset.GetCurrentAmount(chronologicalInflationRates, _timeProvider.Current())
        }));
        
        return financialAssets;
    }
}