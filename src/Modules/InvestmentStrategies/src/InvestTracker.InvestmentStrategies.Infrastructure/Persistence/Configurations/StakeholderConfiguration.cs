using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class StakeholderConfiguration : IEntityTypeConfiguration<Stakeholder>
{
    public void Configure(EntityTypeBuilder<Stakeholder> builder)
    {
        builder.Property(stakeholder => stakeholder.Id)
            .IsRequired()
            .HasConversion(s => s.Value, s => new StakeholderId(s));

        builder.Property(stakeholder => stakeholder.FullName)
            .IsRequired()
            .HasConversion(s => s.Value, s => new FullName(s));
        
        builder.Property(stakeholder => stakeholder.Email)
            .IsRequired()
            .HasConversion(s => s.Value, s => new Email(s));
        
        builder.Property(stakeholder => stakeholder.Subscription)
            .IsRequired()
            .HasConversion(s => s.Value, s => new Subscription(s));
        
        builder.Property(stakeholder => stakeholder.Role)
            .IsRequired()
            .HasConversion(s => s.Value, s => new Role(s));
    }
}