using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities.Stakeholders;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;

public interface ICollaboratorRepository
{
    Task<Collaborator> GetAsync(CollaboratorId id, CancellationToken token = default);
    Task<bool> ExistsAsync(CollaboratorId id, CancellationToken token = default);
    Task AddAsync(Collaborator collaborator, CancellationToken token = default);
    Task DeleteAsync(CollaboratorId id, CancellationToken token = default);
}