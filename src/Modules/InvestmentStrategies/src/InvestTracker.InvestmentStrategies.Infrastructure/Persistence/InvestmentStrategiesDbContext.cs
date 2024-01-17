using InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Entities;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Entities;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Converters;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence;

// dotnet ef database update -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper --context InvestmentStrategiesDbContext
// dotnet ef migrations add <name> -s ..\..\..\..\Bootstrapper\InvestTracker.Bootstrapper --context InvestmentStrategiesDbContext
internal class InvestmentStrategiesDbContext : DbContext
{
    private readonly DomainEventsInterceptor _domainEventsInterceptor;
    
    public DbSet<InvestmentStrategy> InvestmentStrategies { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<EdoTreasuryBond> EdoTreasuryBonds { get; set; }
    public DbSet<CoiTreasuryBond> CoiTreasuryBonds { get; set; }
    public DbSet<Cash> Cash { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Collaboration> Collaborations { get; set; }
    public DbSet<Stakeholder> Stakeholders { get; set; }
    public DbSet<ExchangeRateEntity> ExchangeRates { get; set; }
    public DbSet<InflationRateEntity> InflationRates { get; set; }
    
    public InvestmentStrategiesDbContext(DbContextOptions<InvestmentStrategiesDbContext> options, 
        DomainEventsInterceptor domainEventsInterceptor) : base(options)
    {
        _domainEventsInterceptor = domainEventsInterceptor;
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<FinancialAssetId>()
            .HaveConversion<FinancialAssetIdConverter>();
        
        configurationBuilder
            .Properties<PortfolioId>()
            .HaveConversion<PortfolioIdConverter>();
        
        configurationBuilder
            .Properties<StakeholderId>()
            .HaveConversion<StakeholderIdConverter>();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("investment-strategies");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.Entity<FinancialAsset>().UseTpcMappingStrategy()
            .Ignore(x => x.Currency)
            .Ignore(x => x.Note)
            .Ignore(x => x.AssetName);
        
        modelBuilder.Entity<Cash>().ToTable("FinancialAsset.Cash");
        modelBuilder.Entity<EdoTreasuryBond>().ToTable("FinancialAsset.EdoTreasuryBond");
        modelBuilder.Entity<CoiTreasuryBond>().ToTable("FinancialAsset.CoiTreasuryBond");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_domainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}