namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;

public class StrategyNote
{
    public Guid Id { get; set; }
    public string Note { get; set; }
    public Guid AuthorId { get; set; }
}