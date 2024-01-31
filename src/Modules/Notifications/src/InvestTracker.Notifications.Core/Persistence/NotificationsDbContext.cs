﻿using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Notifications.Core.Persistence;

// dotnet ef database update -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper --context NotificationsDbContext
// dotnet ef migrations add <name> -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper --context NotificationsDbContext
internal class NotificationsDbContext : DbContext
{
    public DbSet<Receiver> Receivers { get; set; }
    public DbSet<PersonalNotificationSetup> PersonalNotificationSetup { get; set; }
    public DbSet<GlobalNotificationSetup> GlobalNotificationSetup { get; set; }
    
    public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("notifications");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.Entity<NotificationSetup>()
            .UseTpcMappingStrategy()
            .ToTable("NotificationSetup");
    }
}