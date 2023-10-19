using InvestTracker.Offers.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Persistence;

// dotnet ef database update -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper --context OffersDbContext
// dotnet ef migrations add <name> -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper --context OffersDbContext
internal class OffersDbContext : DbContext
{
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Advisor> Advisors { get; set; }
    public DbSet<Investor> Investors { get; set; }
    public DbSet<Collaboration> Collaborations { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    
    public OffersDbContext(DbContextOptions<OffersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("offers");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}