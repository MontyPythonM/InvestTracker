using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations.FinancialAssets;

internal class CoiTreasuryBondConfiguration : IEntityTypeConfiguration<CoiTreasuryBond>
{
    public void Configure(EntityTypeBuilder<CoiTreasuryBond> builder)
    {
        builder.ToTable("FinancialAssets.CoiTreasuryBonds");
        
        builder.ComplexProperty(asset => asset.Id)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.Note);
        
        builder.ComplexProperty(asset => asset.Currency)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.FirstYearInterestRate);
        
        builder.ComplexProperty(asset => asset.Margin);

        builder.HasMany(asset => asset.Transactions).WithOne();
    }
}