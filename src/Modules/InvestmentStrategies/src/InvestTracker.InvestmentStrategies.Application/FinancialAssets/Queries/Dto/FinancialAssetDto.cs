namespace InvestTracker.InvestmentStrategies.Application.FinancialAssets.Queries.Dto;

public class FinancialAssetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
}