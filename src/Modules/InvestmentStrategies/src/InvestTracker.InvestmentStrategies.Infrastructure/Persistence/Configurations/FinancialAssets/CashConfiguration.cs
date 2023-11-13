using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations.FinancialAssets;

internal class CashConfiguration : IEntityTypeConfiguration<Cash>
{
    public void Configure(EntityTypeBuilder<Cash> builder)
    {
        builder.ToTable("FinancialAssets.Cash");
        
        builder.Property(asset => asset.Id)
            .HasConversion(a => a.Value, a => new FinancialAssetId(a));
        
        builder.Property(asset => asset.Note)
            .HasConversion(a => a.Value, a => new Note(a));
        
        builder.Property(asset => asset.Currency)
            .IsRequired()
            .HasConversion(a => a.Value, a => new Currency(a));

        builder.HasMany(asset => asset.Transactions).WithOne();
    }
}