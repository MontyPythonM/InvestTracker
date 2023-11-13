using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Infrastructure.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates;

public sealed class ExchangeRate
{
    public Guid Id { get; private set; }
    public Currency From { get; private set; }
    public Currency To { get; private set; }
    public DateOnly Date { get; private set; }
    public DateTime ImportedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedBy { get; private set; }
    public ExchangeRateValue Value { get; private set; }
    public string? Metadata { get; set; }
    
    private ExchangeRate()
    {
    }
    
    public ExchangeRate(Guid id, Currency from, Currency to, DateOnly date, DateTime importedAt, ExchangeRateValue value, 
        string? metadata = null)
    {
        Id = id;
        From = from;
        To = to;
        Date = date;
        ImportedAt = importedAt;
        Value = value;
        Metadata = metadata;
    }

    public void Update(ExchangeRateValue value, string? metadata, DateTime modifiedAt, StakeholderId modifiedBy)
    {
        Value = value;
        Metadata = metadata;
        ModifiedAt = modifiedAt;
        ModifiedBy = modifiedBy.Value;
    }
}