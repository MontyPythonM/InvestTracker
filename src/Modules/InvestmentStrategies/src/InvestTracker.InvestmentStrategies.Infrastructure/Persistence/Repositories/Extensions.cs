using InvestTracker.InvestmentStrategies.Domain.Collaborations.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddScoped<IInvestmentStrategyRepository, InvestmentStrategyRepository>()
            .AddScoped<IStakeholderRepository, StakeholderRepository>()
            .AddScoped<ICollaborationRepository, CollaborationRepository>()
            .AddScoped<IPortfolioRepository, PortfolioRepository>()
            .AddScoped<IExchangeRateRepository, ExchangeRateRepository>()
            .AddScoped<IInflationRateRepository, InflationRateRepository>();
    
    public static IQueryable<Portfolio> ApplyIncludes(this IQueryable<Portfolio> query)
    {
        return query
            .Include(portfolio => portfolio.Cash).ThenInclude(asset => asset.Transactions)
            .Include(portfolio => portfolio.EdoTreasuryBonds).ThenInclude(asset => asset.Transactions)
            .Include(portfolio => portfolio.CoiTreasuryBonds).ThenInclude(asset => asset.Transactions);
    }
    
    public static IQueryable<T> ApplyAsNoTracking<T>(this IQueryable<T> query, bool asNoTracking) 
        where T : class 
        => asNoTracking ? query.AsNoTracking() : query;
}