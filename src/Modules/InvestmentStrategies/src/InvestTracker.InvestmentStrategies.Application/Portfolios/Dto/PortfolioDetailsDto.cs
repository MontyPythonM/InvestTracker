namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;

public class PortfolioDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IEnumerable<FinancialAssetDto> FinancialAssets { get; set; } = new List<FinancialAssetDto>();
}

public class FinancialAssetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal CurrentAmount { get; set; }
}