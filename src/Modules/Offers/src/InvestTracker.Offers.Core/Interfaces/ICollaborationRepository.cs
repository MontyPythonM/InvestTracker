using InvestTracker.Offers.Core.Entities;

namespace InvestTracker.Offers.Core.Interfaces;

public interface ICollaborationRepository
{
    Task<Collaboration?> GetAsync(Guid advisorId, Guid investorId, bool isCancelled = false, CancellationToken token = default);    
    Task CreateAsync(Collaboration collaboration, CancellationToken token = default);
    Task UpdateAsync(Collaboration collaboration, CancellationToken token = default);
}