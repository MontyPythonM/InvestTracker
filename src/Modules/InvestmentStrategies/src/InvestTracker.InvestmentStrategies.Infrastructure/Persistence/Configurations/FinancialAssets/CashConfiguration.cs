using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations.FinancialAssets;

internal class CashConfiguration : IEntityTypeConfiguration<Cash>
{
    public void Configure(EntityTypeBuilder<Cash> builder)
    {
        builder.ToTable("FinancialAssets.Cash");

        builder.ComplexProperty(asset => asset.Id)
            .IsRequired();

        builder.ComplexProperty(asset => asset.Note);
        
        builder.ComplexProperty(asset => asset.Currency)
            .IsRequired();

        builder.HasMany(asset => asset.Transactions).WithOne();
    }
}