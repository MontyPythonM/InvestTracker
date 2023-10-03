using InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;
using InvestTracker.InvestmentStrategies.Domain.Collaborations.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Collaborations.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal sealed class CollaborationRepository : ICollaborationRepository
{
    public Task<Collaboration> GetAsync(CollaborationId id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(CollaborationId id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Collaboration collaboration, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}