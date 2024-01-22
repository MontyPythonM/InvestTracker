using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations.FinancialAssets;

internal class EdoTreasuryBondConfiguration : IEntityTypeConfiguration<EdoTreasuryBond>
{
    public void Configure(EntityTypeBuilder<EdoTreasuryBond> builder)
    {
        builder.ToTable("FinancialAssets.EdoTreasuryBonds");
        
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