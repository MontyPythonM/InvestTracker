namespace InvestTracker.Offers.Core.Exceptions;

internal class CannotCancelNotOwnCollaborationsException : InvalidCastException
{
    public CannotCancelNotOwnCollaborationsException() : base($"User cannot cancel not his own collaborations.")
    {
    }
}