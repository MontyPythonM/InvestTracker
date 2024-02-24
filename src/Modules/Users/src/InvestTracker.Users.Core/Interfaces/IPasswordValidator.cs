namespace InvestTracker.Users.Core.Interfaces;

public interface IPasswordValidator
{
    public string Validate(string password);
    public string Validate(string password, string confirmPassword);
}