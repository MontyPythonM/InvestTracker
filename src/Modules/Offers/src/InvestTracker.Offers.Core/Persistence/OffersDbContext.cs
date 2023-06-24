using InvestTracker.Offers.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Persistence;

// dotnet ef database update -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper
// dotnet ef migrations add Offers_Module_Init -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper
internal class OffersDbContext : DbContext
{
    public DbSet<Offer> Offers { get; set; }
    public DbSet<OfferTag> OfferTags { get; set; }
    public DbSet<Advisor> Advisors { get; set; }
    public DbSet<Investor> Investors { get; set; }
    public DbSet<InvestmentStrategy> InvestmentStrategies { get; set; }
    public DbSet<Collaboration> Collaborations { get; set; }
    
    public OffersDbContext(DbContextOptions<OffersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("offers");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}