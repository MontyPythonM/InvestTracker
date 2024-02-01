using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Entities;

public sealed class InflationRateEntity
{
    public Guid Id { get; private set; }
    public Currency Currency { get; private set; }
    public DateOnly MonthlyDate { get; private set; }
    public DateTime ImportedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedBy { get; private set; }
    public decimal Value { get; private set; }
    public string? Metadata { get; private set; }
    
    private InflationRateEntity()
    {
    }

    public InflationRateEntity(Guid id, Currency currency, MonthlyDate monthlyDate, DateTime importedAt, decimal value, 
        string? metadata = null)
    {
        Id = id;
        Currency = currency;
        MonthlyDate = new DateOnly(monthlyDate.Year, monthlyDate.Month, 01);
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