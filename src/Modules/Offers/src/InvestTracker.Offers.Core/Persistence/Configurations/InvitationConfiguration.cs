using InvestTracker.Offers.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.Offers.Core.Persistence.Configurations;

public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.HasOne(i => i.Offer)
            .WithMany(i => i.Invitations)
            .HasForeignKey(i => i.OfferId);
        
        builder.HasOne(i => i.Sender)
            .WithMany(i => i.Invitations)
            .HasForeignKey(i => i.SenderId);
    }
}