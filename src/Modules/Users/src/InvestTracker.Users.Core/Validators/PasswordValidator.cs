using InvestTracker.Users.Core.Exceptions;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Options;

namespace InvestTracker.Users.Core.Validators;

internal sealed class PasswordValidator : IPasswordValidator
{
    private readonly IEnumerable<Func<string, PasswordValidationOptions, bool>> _validationRules = new List<Func<string, PasswordValidationOptions, bool>>
    {
        HasValidLength,
        HasAtLeastOneSpecialCharacter
    };
    
    private readonly PasswordValidationOptions _passwordValidationOptions;

    public PasswordValidator(PasswordValidationOptions passwordValidationOptions)
    {
        _passwordValidationOptions = passwordValidationOptions;
    }

    public string Validate(string password)
    {
        foreach (var rule in _validationRules)
        {
            if (rule.Invoke(password, _passwordValidationOptions) is false)
            {
                throw new PasswordValidationException(rule.Method);
            }
        }

        return password;
    }

    public string Validate(string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            throw new PasswordValidationException("Passwords are not the same");
        }

        foreach (var rule in _validationRules)
        {
            if (rule.Invoke(password, _passwordValidationOptions) is false)
            {
                throw new PasswordValidationException(rule.Method);
            }
        }

        return password;
    }

    private static bool HasValidLength(string password, PasswordValidationOptions options) 
        => password.Length > options.MinimumLength && password.Length < options.MaximumLength;

    private static bool HasAtLeastOneSpecialCharacter(string password, PasswordValidationOptions options) 
        => password.Any(c => options.SpecialCharacters.Contains(c));
}