using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;

public interface IStakeholderRepository
{
    Task<Stakeholder> GetAsync(StakeholderId id, CancellationToken token = default);
    Task<bool> ExistsAsync(StakeholderId id, CancellationToken token = default);
    Task AddAsync(Stakeholder stakeholder, CancellationToken token = default);
}