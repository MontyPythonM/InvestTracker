using System.Reflection;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal sealed class PasswordValidationException : InvestTrackerException
{
    public PasswordValidationException(MethodInfo method) 
        : base($"Provided password does not meet the validation condition: '{method.Name}'")
    {
    }
    
    public PasswordValidationException(string validationRule) 
        : base($"Provided password does not meet the validation rule: '{validationRule}'")
    {
    }
}