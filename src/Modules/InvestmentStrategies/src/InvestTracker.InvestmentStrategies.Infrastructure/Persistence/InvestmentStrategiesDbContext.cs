using InvestTracker.InvestmentStrategies.Domain.Asset.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence;

internal class InvestmentStrategiesDbContext : DbContext
{
    public DbSet<InvestmentStrategy> InvestmentStrategies { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public InvestmentStrategiesDbContext(DbContextOptions<InvestmentStrategiesDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("investment-strategies");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}