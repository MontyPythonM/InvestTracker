using InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class CollaborationConfiguration : IEntityTypeConfiguration<Collaboration>
{
    public void Configure(EntityTypeBuilder<Collaboration> builder)
    {
        builder.HasKey(collaboration => new { collaboration.PrincipalId, collaboration.AdvisorId });
        
        builder.Property(collaboration => collaboration.PrincipalId)
            .IsRequired()
            .HasConversion(c => c.Value, c => new StakeholderId(c));

        builder.Property(collaboration => collaboration.AdvisorId)
            .IsRequired()
            .HasConversion(c => c.Value, c => new StakeholderId(c));
    }
}