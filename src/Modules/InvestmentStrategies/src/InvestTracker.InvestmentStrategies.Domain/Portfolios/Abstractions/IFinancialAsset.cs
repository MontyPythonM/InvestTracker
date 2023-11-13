using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;

public interface IFinancialAsset
{
    public Currency Currency { get; }
    public Note Note { get; }
    public string GetAssetName();
}