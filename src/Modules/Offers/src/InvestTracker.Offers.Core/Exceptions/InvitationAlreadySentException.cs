using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Offers.Core.Exceptions;

internal class InvitationAlreadySentException : InvestTrackerException
{
    public InvitationAlreadySentException(Guid offerId) : base($"The invitation has already been sent to the offer with ID: {offerId}.")
    {
    }
}