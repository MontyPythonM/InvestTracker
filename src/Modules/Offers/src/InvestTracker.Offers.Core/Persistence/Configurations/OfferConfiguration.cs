using InvestTracker.Offers.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace InvestTracker.Offers.Core.Persistence.Configurations;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.HasOne(offer => offer.Advisor)
            .WithMany(offer => offer.Offers)
            .HasForeignKey(offer => offer.AdvisorId)
            .IsRequired();

        builder.Property(offer => offer.Tags)
            .HasDefaultValue(null)
            .HasConversion(
                tag => JsonConvert.SerializeObject(tag),
                tag => JsonConvert.DeserializeObject<List<string>>(tag)!
            );

        builder.Property(offer => offer.Price)
            .HasPrecision(18, 2);

        builder.Property(offer => offer.Description)
            .HasMaxLength(3000);

        builder.Property(offer => offer.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(offer => offer.CreatedAt)
            .IsRequired();
    }
}