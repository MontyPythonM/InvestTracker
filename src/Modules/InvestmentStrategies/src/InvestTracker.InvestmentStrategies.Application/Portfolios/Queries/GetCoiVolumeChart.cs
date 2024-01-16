using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto.Charts;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;

public record GetCoiVolumeChart(FinancialAssetId FinancialAssetId, PortfolioId PortfolioId) : IQuery<VolumeChart>;