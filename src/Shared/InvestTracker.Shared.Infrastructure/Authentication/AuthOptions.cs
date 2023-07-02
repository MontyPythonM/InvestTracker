namespace InvestTracker.Shared.Infrastructure.Authentication;

internal class AuthOptions
{
    public const string SectionName = "Authentication";
    
    public string IssuerSigningKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string ValidIssuer { get; set; } = string.Empty;
    public IEnumerable<string> Audiences { get; set; } = new List<string>();
    public IEnumerable<string> ValidAudiences { get; set; } = new List<string>();
    public int ExpiryMinutes { get; set; }
    public bool ValidateAudience { get; set; } = true;
    public bool ValidateIssuer { get; set; } = true;
    public bool ValidateLifetime { get; set; } = true;
    public bool SaveToken { get; set; }
    public bool RequireHttpsMetadata { get; set; }
}