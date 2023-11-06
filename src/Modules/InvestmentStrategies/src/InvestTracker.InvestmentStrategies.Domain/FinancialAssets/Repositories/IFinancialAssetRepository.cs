using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Repositories;

public interface IFinancialAssetRepository<T> where T : FinancialAsset
{
    Task<T?> GetAsync(FinancialAssetId id, CancellationToken token = default);
    Task CreateAsync(T asset, CancellationToken token = default);
    Task UpdateAsync(T asset, CancellationToken token = default);
}