using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class InvestmentStrategyConfiguration : IEntityTypeConfiguration<InvestmentStrategy>
{
    public void Configure(EntityTypeBuilder<InvestmentStrategy> builder)
    {
        builder.ComplexProperty(asset => asset.Id)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.Title)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.Note)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.Owner)
            .IsRequired();

        builder.OwnsMany(strategy => strategy.Portfolios);
        
        builder.OwnsMany(strategy => strategy.Collaborators);
    }
}