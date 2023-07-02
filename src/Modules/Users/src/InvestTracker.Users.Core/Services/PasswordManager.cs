using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InvestTracker.Users.Core.Services;

internal sealed class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordManager(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }
    
    public string Secure(string password) 
        => _passwordHasher.HashPassword(default!, password);

    public bool Validate(string password, string hashedPassword)
        => _passwordHasher.VerifyHashedPassword(default!, hashedPassword, password) == 
           PasswordVerificationResult.Success;
}