namespace InvestTracker.Offers.Core.Exceptions;

public class CannotConfirmNotOwnCollaborationsException : InvalidCastException
{
    public CannotConfirmNotOwnCollaborationsException() : base($"User cannot confirm not his own collaboration.")
    {
    }
}