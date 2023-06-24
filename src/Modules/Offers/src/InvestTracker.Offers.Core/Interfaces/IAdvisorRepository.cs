using InvestTracker.Offers.Core.Entities;

namespace InvestTracker.Offers.Core.Interfaces;

public interface IAdvisorRepository
{
    Task<Advisor?> GetAsync(Guid advisorId, CancellationToken token);
    Task CreateAsync(Advisor advisor, CancellationToken token);
    Task UpdateAsync(Advisor advisor, CancellationToken token);
    Task DeleteAsync(Advisor advisor, CancellationToken token);
}