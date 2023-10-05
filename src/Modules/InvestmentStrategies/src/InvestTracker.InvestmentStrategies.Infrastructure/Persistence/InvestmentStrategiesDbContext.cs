using InvestTracker.InvestmentStrategies.Domain.Asset.Entities;
using InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence;

// dotnet ef database update -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper
// dotnet ef migrations add <name> -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper --context InvestmentStrategiesDbContext
internal class InvestmentStrategiesDbContext : DbContext
{
    public DbSet<InvestmentStrategy> InvestmentStrategies { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Collaboration> Collaborations { get; set; }
    public DbSet<Stakeholder> Stakeholders { get; set; }
    public DbSet<ExchangeRate> ExchangeRates { get; set; }
    
    public InvestmentStrategiesDbContext(DbContextOptions<InvestmentStrategiesDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("investment-strategies");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}