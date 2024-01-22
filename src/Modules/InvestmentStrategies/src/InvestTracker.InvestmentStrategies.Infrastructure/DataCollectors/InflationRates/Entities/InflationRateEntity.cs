using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Entities;

public sealed class InflationRateEntity
{
    public Guid Id { get; private set; }
    public Currency Currency { get; }
    public MonthlyDate MonthlyDate { get; set; }
    public DateTime ImportedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedBy { get; private set; }
    public decimal Value { get; private set; }
    public string? Metadata { get; set; }
    
    private InflationRateEntity()
    {
    }

    public InflationRateEntity(Guid id, Currency currency, MonthlyDate monthlyDate, DateTime importedAt, decimal value, 
        string? metadata = null)
    {
        Id = id;
        Currency = currency;
        MonthlyDate = monthlyDate;
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