using InvestTracker.Offers.Core.Entities;

namespace InvestTracker.Offers.Core.Interfaces;

public interface IInvitationRepository
{
    Task<Invitation?> GetAsync(Guid id, CancellationToken token);    
    Task CreateAsync(Invitation invitation, CancellationToken token);
    Task UpdateAsync(Invitation invitation, CancellationToken token);
}