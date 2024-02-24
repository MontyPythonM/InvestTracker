using System.Runtime.CompilerServices;
using InvestTracker.Shared.Infrastructure;
using InvestTracker.Shared.Infrastructure.Postgres;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Options;
using InvestTracker.Users.Core.Persistence;
using InvestTracker.Users.Core.Persistence.Repositories;
using InvestTracker.Users.Core.Services;
using InvestTracker.Users.Core.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Users.Api")]
[assembly: InternalsVisibleTo("InvestTracker.Users.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace InvestTracker.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        var passwordResetOptions = services.GetOptions<PasswordResetOptions>("Password:Reset");
        var passwordValidationOptions = services.GetOptions<PasswordValidationOptions>("Password:ValidationRules");
        
        services
            .AddPostgres<UsersDbContext>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>()
            .AddTransient<IAccountService, AccountService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddSingleton(passwordResetOptions)
            .AddSingleton(passwordValidationOptions)
            .AddSingleton<IPasswordValidator, PasswordValidator>();
        
        return services;
    }
}