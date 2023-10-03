﻿using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.PortfolioLimitPolicy;

internal class AdvisorPortfolioLimitPolicy : IPortfolioLimitPolicy
{
    private const int AdvisorPortfolioLimit = 20;
    
    public bool CanBeApplied(Subscription subscription)
        => subscription == SystemSubscription.Advisor;

    public bool CanAddPortfolio(ISet<Portfolio> portfolios)
        => portfolios.Count >= AdvisorPortfolioLimit;
}