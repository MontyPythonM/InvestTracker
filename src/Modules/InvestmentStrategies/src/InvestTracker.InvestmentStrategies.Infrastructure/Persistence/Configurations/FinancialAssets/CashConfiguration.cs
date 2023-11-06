using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations.FinancialAssets;

internal class CashConfiguration : IEntityTypeConfiguration<Cash>
{
    public void Configure(EntityTypeBuilder<Cash> builder)
    {
        builder.ToTable("FinancialAssets.Cash");
        
        builder.HasMany(asset => asset.Transactions).WithOne();
    }
}