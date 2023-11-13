namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Dto;

public class InvestmentStrategiesDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
}