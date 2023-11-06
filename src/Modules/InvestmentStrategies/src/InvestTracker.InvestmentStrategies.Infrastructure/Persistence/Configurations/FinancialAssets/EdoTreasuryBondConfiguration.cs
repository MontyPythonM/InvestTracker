using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Assets;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations.FinancialAssets;

internal class EdoTreasuryBondConfiguration : IEntityTypeConfiguration<EdoTreasuryBond>
{
    public void Configure(EntityTypeBuilder<EdoTreasuryBond> builder)
    {
        builder.ToTable("FinancialAssets.EdoTreasuryBonds");
        
        builder.Property(asset => asset.FirstYearInterestRate)
            .HasConversion(a => a.Value, a => new InterestRate(a));
        
        builder.Property(asset => asset.Margin)
            .HasConversion(a => a.Value, a => new Margin(a));

        builder.HasMany(asset => asset.Transactions).WithOne();
    }
}