﻿using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRateEntity>
{
    public void Configure(EntityTypeBuilder<ExchangeRateEntity> builder)
    {
        builder.HasIndex(exchangeRate => exchangeRate.Date);
        builder.HasIndex(exchangeRate => exchangeRate.To);

        builder.Property(exchangeRate => exchangeRate.From)
            .IsRequired()
            .HasConversion(e => e.Value, e => new Currency(e));
        
        builder.Property(exchangeRate => exchangeRate.To)
            .IsRequired()
            .HasConversion(e => e.Value, e => new Currency(e));

        builder.Property(exchangeRate => exchangeRate.Date)
            .IsRequired()
            .HasConversion(dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), 
                dateTime => DateOnly.FromDateTime(dateTime));

        builder.Property(exchangeRate => exchangeRate.Value)
            .IsRequired()
            .HasPrecision(12, 4);

        builder.Property(exchangeRate => exchangeRate.Metadata)
            .HasMaxLength(1500);
    }
}