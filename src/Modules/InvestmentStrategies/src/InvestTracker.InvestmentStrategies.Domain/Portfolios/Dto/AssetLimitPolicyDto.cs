using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Dto;

public sealed record AssetLimitPolicyDto(Subscription Subscription, IEnumerable<IFinancialAssetLimitPolicy> Policies);