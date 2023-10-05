using InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;
using InvestTracker.InvestmentStrategies.Domain.Collaborations.ValueObjects.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class CollaborationConfiguration : IEntityTypeConfiguration<Collaboration>
{
    public void Configure(EntityTypeBuilder<Collaboration> builder)
    {
        builder.HasKey(collaboration => collaboration.Id);
        
        builder.Property(exchangeRate => exchangeRate.Id)
            .IsRequired()
            .HasConversion(e => new { e.AdvisorId, e.PrincipalId }, 
                e => new CollaborationId(e.AdvisorId, e.PrincipalId));
    }
}

// builder.Property(collaboration => collaboration.Id.PrincipalId)
//     .IsRequired()
//     .HasColumnName("PrincipalId");
//
// builder.Property(collaboration => collaboration.Id.AdvisorId)
//     .IsRequired()
//     .HasColumnName("AdvisorId");