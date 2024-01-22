using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class InflationRateConfiguration : IEntityTypeConfiguration<InflationRateEntity>
{
    public void Configure(EntityTypeBuilder<InflationRateEntity> builder)
    {
        builder.ComplexProperty(asset => asset.Currency)
            .IsRequired();

        builder.Property(rate => rate.Value)
            .IsRequired()
            .HasPrecision(12, 4);

        builder.ComplexProperty(rate => rate.MonthlyDate)
            .IsRequired();
        
        builder.Property(rate => rate.Metadata)
            .HasMaxLength(1500);
        
        builder.HasIndex(rate => rate.Currency);
        builder.HasIndex(rate => rate.MonthlyDate);
    }
}