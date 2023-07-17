namespace InvestTracker.Offers.Core.Exceptions;

internal class CannotConfirmNotOwnCollaborationsException : InvalidCastException
{
    public CannotConfirmNotOwnCollaborationsException() : base($"User cannot confirm not his own collaboration.")
    {
    }
}