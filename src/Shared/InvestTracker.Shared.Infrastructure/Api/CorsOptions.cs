﻿namespace InvestTracker.Shared.Infrastructure.Api;

public class CorsOptions
{
    public const string SectionName = "CorsPolicy";
    public bool AllowCredentials { get; set; }
    public IEnumerable<string>? AllowedOrigins { get; set; }
    public IEnumerable<string>? AllowedMethods { get; set; }
    public IEnumerable<string>? AllowedHeaders { get; set; }
}