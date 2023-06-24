using InvestTracker.Offers.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.Offers.Core.Persistence.Configurations;

public class AdvisorConfiguration : IEntityTypeConfiguration<Advisor>
{
    public void Configure(EntityTypeBuilder<Advisor> builder)
    {
        builder.Property(advisor => advisor.FullName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(advisor => advisor.Email)
            .IsRequired();

        builder.Property(advisor => advisor.Bio)
            .HasMaxLength(3000);
    }
}