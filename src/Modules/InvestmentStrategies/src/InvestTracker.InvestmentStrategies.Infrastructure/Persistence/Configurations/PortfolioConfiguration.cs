﻿using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.Property(portfolio => portfolio.Id)
            .IsRequired()
            .HasConversion(p => p.Value, p => new PortfolioId(p));
        
        builder.Property(portfolio => portfolio.Title)
            .IsRequired()
            .HasConversion(p => p.Value, p => new Title(p));
        
        builder.Property(portfolio => portfolio.Note)
            .HasConversion(p => p.Value, p => new Note(p));
        
        builder.Property(portfolio => portfolio.Description)
            .HasConversion(p => p.Value, p => new Description(p));

        builder.Property(portfolio => portfolio.InvestmentStrategyId)
            .IsRequired()
            .HasConversion(p => p.Value, p => new InvestmentStrategyId(p));
    }
}