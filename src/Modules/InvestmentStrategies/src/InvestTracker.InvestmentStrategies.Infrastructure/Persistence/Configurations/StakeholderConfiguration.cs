using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class StakeholderConfiguration : IEntityTypeConfiguration<Stakeholder>
{
    public void Configure(EntityTypeBuilder<Stakeholder> builder)
    {
        builder.ComplexProperty(asset => asset.Id)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.FullName)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.Email)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.Subscription)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.Role)
            .IsRequired();
    }
}