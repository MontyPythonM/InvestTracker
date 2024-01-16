using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations.FinancialAssets;

internal class CoiTreasuryBondConfiguration : IEntityTypeConfiguration<CoiTreasuryBond>
{
    public void Configure(EntityTypeBuilder<CoiTreasuryBond> builder)
    {
        builder.ToTable("FinancialAssets.CoiTreasuryBonds");
        
        builder.Property(asset => asset.Id)
            .HasConversion(a => a.Value, a => new FinancialAssetId(a));
        
        builder.Property(asset => asset.Note)
            .HasConversion(a => a.Value, a => new Note(a));
        
        builder.Property(asset => asset.Currency)
            .IsRequired()
            .HasConversion(a => a.Value, a => new Currency(a));
        
        builder.Property(asset => asset.FirstYearInterestRate)
            .HasConversion(a => a.Value, a => new InterestRate(a));
        
        builder.Property(asset => asset.Margin)
            .HasConversion(a => a.Value, a => new Margin(a));

        builder.HasMany(asset => asset.Transactions).WithOne();
    }
}