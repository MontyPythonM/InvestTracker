using InvestTracker.Offers.Core.Entities;

namespace InvestTracker.Offers.Core.Interfaces;

public interface IInvestorRepository
{
    Task<Investor?> GetAsync(Guid id, CancellationToken token);
    Task CreateAsync(Investor investor, CancellationToken token);
    Task<bool> ExistsAsync(Guid id, CancellationToken token);
}