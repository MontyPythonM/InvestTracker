namespace InvestTracker.Shared.Infrastructure.Postgres;

internal sealed class PostgresOptions
{
    public const string SectionName = "Postgres";
    public string Host { get; set; } = "database";
    public int Port { get; set; } = 5432;
    public string Database { get; set; } = "database";
    public string Username { get; set; } = "postgres";
    public string Password { get; set; } = "postgres";
    public string ConnectionString => $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password};";
}