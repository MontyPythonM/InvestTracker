using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;
using InvestTracker.Shared.Abstractions.Context;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Authorization;

internal sealed class ResourceAccessor : IResourceAccessor
{
    private readonly IInvestmentStrategyRepository _strategyRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IRequestContext _requestContext;
    
    public ResourceAccessor(IInvestmentStrategyRepository strategyRepository, IPortfolioRepository portfolioRepository, IRequestContext requestContext)
    {
        _strategyRepository = strategyRepository;
        _portfolioRepository = portfolioRepository;
        _requestContext = requestContext;
    }

    public async Task<bool> CheckAsync(InvestmentStrategyId strategyId, CancellationToken token = default)
    {
        if (!await HasAccessAsync(strategyId, token))
            throw new InvestmentStrategyAccessException(strategyId);

        return true;
    }

    public async Task<bool> CheckAsync(PortfolioId portfolioId, CancellationToken token = default)
    {
        if (!await HasAccessAsync(portfolioId, token))
            throw new PortfolioAccessException(portfolioId);
        
        return true;
    }
    
    public bool Check(InvestmentStrategy strategy)
    {
        if (!HasAccess(strategy))
            throw new InvestmentStrategyAccessException(strategy.Id);

        return true;
    }
    
    public async Task<bool> HasAccessAsync(InvestmentStrategyId strategyId, CancellationToken token = default)
        => await _strategyRepository.HasAccessAsync(strategyId, _requestContext.Identity.UserId, token);
    
    public async Task<bool> HasAccessAsync(PortfolioId portfolioId, CancellationToken token = default)
        => await _portfolioRepository.HasAccessAsync(portfolioId, _requestContext.Identity.UserId, token);
    
    public bool HasAccess(InvestmentStrategy strategy)
        => strategy.IsStakeholderHaveAccess(_requestContext.Identity.UserId);
}