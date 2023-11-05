using InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence;

// dotnet ef database update -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper --context InvestmentStrategiesDbContext
// dotnet ef migrations add <name> -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper --context InvestmentStrategiesDbContext
internal class InvestmentStrategiesDbContext : DbContext
{
    public DbSet<InvestmentStrategy> InvestmentStrategies { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<FinancialAsset> FinancialAssets { get; set; }
    public DbSet<AmountTransaction> AmountTransactions { get; set; }
    public DbSet<VolumeTransaction> VolumeTransactions { get; set; }
    public DbSet<Collaboration> Collaborations { get; set; }
    public DbSet<Stakeholder> Stakeholders { get; set; }
    public DbSet<ExchangeRate> ExchangeRates { get; set; }
    
    public InvestmentStrategiesDbContext(DbContextOptions<InvestmentStrategiesDbContext> options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<FinancialAssetId>()
            .HaveConversion<FinancialAssetIdConverter>();
        
        configurationBuilder
            .Properties<StakeholderId>()
            .HaveConversion<StakeholderIdConverter>();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("investment-strategies");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        
        modelBuilder.Entity<FinancialAsset>().UseTpcMappingStrategy();
    }
}