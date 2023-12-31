﻿using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class InvestmentStrategyConfiguration : IEntityTypeConfiguration<InvestmentStrategy>
{
    public void Configure(EntityTypeBuilder<InvestmentStrategy> builder)
    {
        builder.Property(strategy => strategy.Id)
            .IsRequired()
            .HasConversion(s => s.Value, s => new InvestmentStrategyId(s));
        
        builder.Property(strategy => strategy.Title)
            .IsRequired()
            .HasConversion(s => s.Value, s => new Title(s));
        
        builder.Property(strategy => strategy.Note)
            .HasConversion(s => s.Value, s => new Note(s));
        
        builder.Property(strategy => strategy.Owner)
            .IsRequired()
            .HasConversion(s => s.Value, s => new StakeholderId(s));

        builder.OwnsMany(strategy => strategy.Portfolios);
        
        builder.OwnsMany(strategy => strategy.Collaborators);
    }
}