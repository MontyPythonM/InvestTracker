using InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Collaborations.Repositories;

public interface ICollaborationRepository
{
    Task<Collaboration> GetAsync(StakeholderId advisorId, StakeholderId principalId, CancellationToken token = default);
    Task<bool> ExistsAsync(StakeholderId advisorId, StakeholderId principalId, CancellationToken token = default);
    Task AddAsync(Collaboration collaboration, CancellationToken token = default);
}