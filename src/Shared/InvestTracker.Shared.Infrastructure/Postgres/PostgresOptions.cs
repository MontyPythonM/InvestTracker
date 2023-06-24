namespace InvestTracker.Shared.Infrastructure.Postgres;

internal sealed class PostgresOptions
{
    public const string SectionName = "Postgres";
    public string ConnectionString { get; set; } = string.Empty;
}