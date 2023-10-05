using InvestTracker.InvestmentStrategies.Domain.Asset.Entities;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.Property(asset => asset.Id)
            .IsRequired()
            .HasConversion(a => a.Value, a => new AssetId(a));
        
        builder.Property(asset => asset.Note)
            .HasConversion(a => a.Value, a => new Note(a));
        
        builder.Property(asset => asset.Currency)
            .IsRequired()
            .HasConversion(a => a.Value, a => new Currency(a));
        
        builder.Property(asset => asset.AssetDataId)
            .IsRequired()
            .HasConversion(a => a.Value, a => new AssetDataId(a));
        
        builder.Property(asset => asset.PortfolioId)
            .IsRequired()
            .HasConversion(a => a.Value, a => new PortfolioId(a));

        builder.HasMany(asset => asset.Transactions).WithOne();
    }
}