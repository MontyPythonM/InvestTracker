using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal sealed class StakeholderRepository : IStakeholderRepository
{
    public Task<Stakeholder> GetAsync(StakeholderId id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(StakeholderId id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Stakeholder stakeholder, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}