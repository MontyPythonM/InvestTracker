using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Entities;

public sealed class ExchangeRateEntity
{
    public Guid Id { get; private set; }
    public Currency From { get; private set; }
    public Currency To { get; private set; }
    public DateOnly Date { get; private set; }
    public DateTime ImportedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedBy { get; private set; }
    public decimal Value { get; private set; }
    public string? Metadata { get; private set; }
    
    private ExchangeRateEntity()
    {
    }
    
    public ExchangeRateEntity(Guid id, Currency from, Currency to, DateOnly date, DateTime importedAt, decimal value, 
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

    public void Update(decimal value, string? metadata, DateTime modifiedAt, StakeholderId modifiedBy)
    {
        Value = value;
        Metadata = metadata;
        ModifiedAt = modifiedAt;
        ModifiedBy = modifiedBy.Value;
    }
}