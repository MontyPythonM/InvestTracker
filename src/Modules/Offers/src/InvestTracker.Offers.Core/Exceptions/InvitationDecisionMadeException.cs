using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Offers.Core.Exceptions;

internal class InvitationDecisionMadeException : InvestTrackerException
{
    public InvitationDecisionMadeException(Guid id) : base($"Decision has already been made for the invitation with ID: {id}.")
    {
    }
}