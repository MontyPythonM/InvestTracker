using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates;

internal sealed class ExchangeRate
{
    public Guid Id { get; private set; }
    public Currency From { get; private set; }
    public Currency To { get; private set; }
    public DateOnly Date { get; private set; }
    public DateTime ImportedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public StakeholderId? ModifiedBy { get; private set; }
    public decimal Value { get; private set; }
    public string? Metadata { get; set; }
    
    private ExchangeRate()
    {
    }
    
    public ExchangeRate(Guid id, Currency from, Currency to, DateOnly date, DateTime importedAt, Amount amount, string? metadata = null)
    {
        Id = id;
        From = from;
        To = to;
        Date = date;
        ImportedAt = importedAt;
        Value = amount;
        Metadata = metadata;
    }

    public void Update(Amount amount, string? metadata, DateTime modifiedAt, StakeholderId modifiedBy)
    {
        Value = amount;
        Metadata = metadata;
        ModifiedAt = modifiedAt;
        ModifiedBy = modifiedBy;
    }
}