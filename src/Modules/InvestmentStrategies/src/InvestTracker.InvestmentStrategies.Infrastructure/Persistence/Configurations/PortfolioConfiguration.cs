using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.ComplexProperty(asset => asset.Id)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.Title)
            .IsRequired();

        builder.ComplexProperty(asset => asset.Note);

        builder.ComplexProperty(asset => asset.Description);
        
        builder.ComplexProperty(asset => asset.InvestmentStrategyId)
            .IsRequired();
    }
}