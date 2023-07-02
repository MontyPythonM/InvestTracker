using System.Runtime.CompilerServices;
using InvestTracker.Shared.Infrastructure.Postgres;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Persistence;
using InvestTracker.Users.Core.Persistence.Repositories;
using InvestTracker.Users.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Users.Api")]
namespace InvestTracker.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services
            .AddPostgres<UsersDbContext>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>()
            .AddTransient<IAccountService, AccountService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}