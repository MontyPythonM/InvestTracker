using InvestTracker.InvestmentStrategies.Application.FinancialAssets.Queries.Dto;
using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.InvestmentStrategies.Application.FinancialAssets.Queries;

public record GetPortfolioFinancialAssets(Guid PortfolioId) : IQuery<IEnumerable<FinancialAssetDto>>;