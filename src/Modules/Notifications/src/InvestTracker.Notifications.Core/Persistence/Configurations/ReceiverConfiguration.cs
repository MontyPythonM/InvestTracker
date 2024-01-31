﻿using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.Notifications.Core.Persistence.Configurations;

internal class ReceiverConfiguration : IEntityTypeConfiguration<Receiver>
{
    public void Configure(EntityTypeBuilder<Receiver> builder)
    {
        builder.Property(n => n.Email)
            .HasMaxLength(100)
            .IsRequired()
            .HasConversion(n => n.Value, u => new Email(u));

        builder.Property(n => n.PhoneNumber)
            .HasConversion(n => n.Value, n => new PhoneNumber(n));
        
        builder.Property(n => n.Subscription)
            .HasConversion(n => n.Value, n => new Subscription(n));
        
        builder.Property(n => n.Role)
            .HasConversion(n => n.Value, n => new Role(n));

        builder.HasOne(n => n.NotificationSetup).WithOne();
    }
}