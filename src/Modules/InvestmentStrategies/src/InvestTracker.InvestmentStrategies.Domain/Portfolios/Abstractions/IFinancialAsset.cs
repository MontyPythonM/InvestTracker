using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;

public interface IFinancialAsset
{
    public FinancialAssetId Id { get; }
    public Currency Currency { get; }
    public Note Note { get; }
    public PortfolioId PortfolioId { get; }
    public string GetAssetName();
}