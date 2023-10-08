using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates;
using InvestTracker.InvestmentStrategies.Infrastructure.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasIndex(exchangeRate => exchangeRate.Date);
        builder.HasIndex(exchangeRate => exchangeRate.To);

        builder.Property(exchangeRate => exchangeRate.From)
            .IsRequired()
            .HasConversion(e => e.Value, e => new Currency(e));
        
        builder.Property(exchangeRate => exchangeRate.To)
            .IsRequired()
            .HasConversion(e => e.Value, e => new Currency(e));

        builder.Property(exchangeRate => exchangeRate.Date)
            .IsRequired()
            .HasConversion(dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), 
                dateTime => DateOnly.FromDateTime(dateTime));

        builder.Property(exchangeRate => exchangeRate.Value)
            .IsRequired()
            .HasPrecision(12, 4)
            .HasConversion(e => e.Value, e => new ExchangeRateValue(e));

        builder.Property(exchangeRate => exchangeRate.Metadata)
            .HasMaxLength(1500);
    }
}