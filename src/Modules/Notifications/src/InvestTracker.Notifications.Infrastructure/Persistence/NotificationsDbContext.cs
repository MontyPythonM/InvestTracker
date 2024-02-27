using InvestTracker.Notifications.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Notifications.Infrastructure.Persistence;

// dotnet ef database update -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper --context NotificationsDbContext
// dotnet ef migrations add <name> -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper --context NotificationsDbContext
internal class NotificationsDbContext : DbContext
{
    public DbSet<Receiver> Receivers { get; set; }
    public DbSet<GlobalSettings> GlobalSettings { get; set; }
    
    public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("notifications");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}