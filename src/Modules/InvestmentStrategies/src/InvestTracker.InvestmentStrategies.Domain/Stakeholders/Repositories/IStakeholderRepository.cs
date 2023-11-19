using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;

public interface IStakeholderRepository
{
    Task<Stakeholder?> GetAsync(StakeholderId id, CancellationToken token = default);
    Task<bool> ExistsAsync(StakeholderId id, CancellationToken token = default);
    Task<Subscription?> GetSubscriptionAsync(StakeholderId id, CancellationToken token = default);
    Task<Subscription?> GetOwnerSubscription(PortfolioId portfolioId, CancellationToken token = default);
    Task AddAsync(Stakeholder stakeholder, CancellationToken token = default);
    Task UpdateAsync(Stakeholder stakeholder, CancellationToken token = default);
}