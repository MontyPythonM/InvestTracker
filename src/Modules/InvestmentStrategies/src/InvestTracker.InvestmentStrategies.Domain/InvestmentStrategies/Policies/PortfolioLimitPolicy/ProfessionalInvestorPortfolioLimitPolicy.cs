﻿using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.PortfolioLimitPolicy;

internal class ProfessionalInvestorPortfolioLimitPolicy : IPortfolioLimitPolicy
{
    private const int ProfessionalInvestorPortfolioLimit = 5;

    public bool CanBeApplied(Subscription subscription)
        => subscription == SystemSubscription.ProfessionalInvestor;

    public bool CanAddPortfolio(ISet<Portfolio> portfolios)
        => portfolios.Count >= ProfessionalInvestorPortfolioLimit;
}