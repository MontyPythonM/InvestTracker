using InvestTracker.Offers.Core.Entities;

namespace InvestTracker.Offers.Core.Interfaces;

public interface IAdvisorRepository
{
    Task<Advisor?> GetAsync(Guid advisorId, CancellationToken token = default);
    Task CreateAsync(Advisor advisor, CancellationToken token = default);
    Task UpdateAsync(Advisor advisor, CancellationToken token = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken token = default);
    Task DeleteAsync(Advisor advisor, CancellationToken token = default);
}