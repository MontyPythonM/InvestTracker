using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.PortfolioLimitPolicy;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;

public sealed record PortfolioPolicyLimitDto(ISet<Portfolio> ExistingPortfolios, Subscription OwnerSubscription, 
    IEnumerable<IPortfolioLimitPolicy> Policies);