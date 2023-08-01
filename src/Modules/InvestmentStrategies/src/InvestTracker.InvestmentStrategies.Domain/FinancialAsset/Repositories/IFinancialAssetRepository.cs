using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAsset.Repositories;

public interface IFinancialAssetRepository
{
    Task<Entities.FinancialAsset?> GetAsync(FinancialAssetId id, CancellationToken token = default);
    Task AddAsync(Entities.FinancialAsset financialAsset, CancellationToken token = default);
}