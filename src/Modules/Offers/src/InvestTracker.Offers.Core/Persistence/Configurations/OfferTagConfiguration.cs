using InvestTracker.Offers.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.Offers.Core.Persistence.Configurations;

public class OfferTagConfiguration : IEntityTypeConfiguration<OfferTag>
{
    public void Configure(EntityTypeBuilder<OfferTag> builder)
    {
        builder.Property(tag => tag.Value)
            .HasMaxLength(50)
            .IsRequired();
    }
}