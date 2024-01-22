using InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class CollaborationConfiguration : IEntityTypeConfiguration<Collaboration>
{
    public void Configure(EntityTypeBuilder<Collaboration> builder)
    {
        builder.HasKey(collaboration => new { collaboration.PrincipalId, collaboration.AdvisorId });
        
        builder.ComplexProperty(asset => asset.PrincipalId)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.AdvisorId)
            .IsRequired();
    }
}