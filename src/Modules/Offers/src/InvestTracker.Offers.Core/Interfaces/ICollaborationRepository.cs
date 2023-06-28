using InvestTracker.Offers.Core.Entities;

namespace InvestTracker.Offers.Core.Interfaces;

public interface ICollaborationRepository
{
    Task<Collaboration?> GetAsync(Guid advisorId, Guid investorId, CancellationToken token);    
    Task CreateAsync(Collaboration collaboration, CancellationToken token);
    Task UpdateAsync(Collaboration collaboration, CancellationToken token);
}