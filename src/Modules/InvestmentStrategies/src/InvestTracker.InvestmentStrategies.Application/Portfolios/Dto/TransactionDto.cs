namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;

public class TransactionDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Note { get; set; }
}