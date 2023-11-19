using InvestTracker.Offers.Core.Entities;

namespace InvestTracker.Offers.Core.Interfaces;

public interface IInvestorRepository
{
    Task<Investor?> GetAsync(Guid id, CancellationToken token = default);
    Task CreateAsync(Investor investor, CancellationToken token = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken token = default);
    Task DeleteAsync(Investor investor, CancellationToken token = default);
}