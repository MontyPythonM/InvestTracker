namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;

public class CoiBondDto
{
    public Guid Id { get; set; }
    public string Symbol { get; set; }
    public string Currency { get; set; }
    public decimal Margin { get; set; }
    public decimal CurrentCumulativeAmount { get; set; }
    public decimal CurrentPeriodAmount { get; set; }
    public int CurrentVolume { get; set; }
    public string Note { get; set; }
    public DateOnly PurchaseDate { get; set; }
    public DateOnly RedemptionDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal CumulativeInterestRate { get; set; }
    public IEnumerable<decimal> PeriodInterestRates { get; set; }
    public IEnumerable<TransactionDto> Transactions { get; set; }
}