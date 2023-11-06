using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Repositories;

public interface IFinancialAssetRepository
{
    Task<T?> GetAsync<T>(FinancialAssetId id, CancellationToken token = default) where T : FinancialAsset;
    Task<IEnumerable<FinancialAsset>> GetAssetsAsync(PortfolioId portfolioId, CancellationToken token = default);
    Task CreateAsync(FinancialAsset asset, CancellationToken token = default);
    Task UpdateAsync(FinancialAsset asset, CancellationToken token = default);
}