namespace InvestTracker.Users.Core.Interfaces;

public interface IPasswordManager
{
    string Secure(string password);
    bool Validate(string password, string hashedPassword);
}