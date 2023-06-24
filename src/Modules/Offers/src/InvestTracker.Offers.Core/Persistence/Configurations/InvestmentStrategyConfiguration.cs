using InvestTracker.Offers.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.Offers.Core.Persistence.Configurations;

public class InvestmentStrategyConfiguration : IEntityTypeConfiguration<InvestmentStrategy>
{
    public void Configure(EntityTypeBuilder<InvestmentStrategy> builder)
    {
        builder.Property(inv => inv.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(inv => inv.CreatedAt)
            .IsRequired();

        builder.Property(inv => inv.OwnerId)
            .IsRequired();
    }
}