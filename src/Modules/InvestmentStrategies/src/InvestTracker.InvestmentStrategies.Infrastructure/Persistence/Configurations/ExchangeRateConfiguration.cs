using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRateEntity>
{
    public void Configure(EntityTypeBuilder<ExchangeRateEntity> builder)
    {
        builder.HasIndex(exchangeRate => exchangeRate.Date);
        builder.HasIndex(exchangeRate => exchangeRate.To);

        builder.ComplexProperty(asset => asset.From)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.To)
            .IsRequired();

        builder.Property(exchangeRate => exchangeRate.Date)
            .IsRequired();

        builder.Property(exchangeRate => exchangeRate.Value)
            .IsRequired()
            .HasPrecision(12, 4);

        builder.Property(exchangeRate => exchangeRate.Metadata)
            .HasMaxLength(1500);
    }
}