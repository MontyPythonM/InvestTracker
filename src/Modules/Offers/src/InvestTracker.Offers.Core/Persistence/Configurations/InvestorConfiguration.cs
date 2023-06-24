using InvestTracker.Offers.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.Offers.Core.Persistence.Configurations;

public class InvestorConfiguration : IEntityTypeConfiguration<Investor>
{
    public void Configure(EntityTypeBuilder<Investor> builder)
    {
        builder.Property(investor => investor.FullName)
            .HasMaxLength(100)
            .IsRequired();
    }
}