using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class FinancialAssetConfiguration : IEntityTypeConfiguration<FinancialAsset>
{
    public void Configure(EntityTypeBuilder<FinancialAsset> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(asset => asset.Id)
            .HasConversion(a => a.Value, a => new FinancialAssetId(a));
        
        builder.Property(asset => asset.Note)
            .HasConversion(a => a.Value, a => new Note(a));
        
        builder.Property(asset => asset.Currency)
            .IsRequired()
            .HasConversion(a => a.Value, a => new Currency(a));

        builder.Property(asset => asset.PortfolioId)
            .IsRequired()
            .HasConversion(a => a.Value, a => new PortfolioId(a));
    }
}