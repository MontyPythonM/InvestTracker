﻿using System.Runtime.CompilerServices;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Offers.Core.Persistence;
using InvestTracker.Offers.Core.Persistence.Repositories;
using InvestTracker.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Offers.Api")]
namespace InvestTracker.Offers.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<OffersDbContext>();
        
        services.AddScoped<IOfferRepository, OfferRepository>()
            .AddScoped<IAdvisorRepository, AdvisorRepository>();
        
        return services;
    }
}