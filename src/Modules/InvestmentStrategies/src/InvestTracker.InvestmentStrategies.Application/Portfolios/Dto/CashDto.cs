namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;

public class CashDto
{
    public Guid Id { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<TransactionDto> Transactions { get; set; }
}