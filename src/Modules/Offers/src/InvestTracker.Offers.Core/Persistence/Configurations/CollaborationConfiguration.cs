using InvestTracker.Offers.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.Offers.Core.Persistence.Configurations;

public class CollaborationConfiguration : IEntityTypeConfiguration<Collaboration>
{
    public void Configure(EntityTypeBuilder<Collaboration> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.HasIndex(c => new { c.InvestorId, c.AdvisorId });
        
        builder.HasOne(c => c.Advisor).WithMany(c => c.Collaborations);
        builder.HasOne(c => c.Investor).WithMany(c => c.Collaborations);
    }
}