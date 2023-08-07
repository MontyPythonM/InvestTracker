using InvestTracker.InvestmentStrategies.Domain.Collaborations.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Collaborations.Repositories;

public interface ICollaborationRepository
{
    Task<Collaborations.Entities.Collaboration> GetAsync(CollaborationId id, CancellationToken token = default);
    Task<bool> ExistsAsync(CollaborationId id, CancellationToken token = default);
    Task AddAsync(Collaborations.Entities.Collaboration collaboration, CancellationToken token = default);
}