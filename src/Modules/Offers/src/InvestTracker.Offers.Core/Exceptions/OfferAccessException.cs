using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Offers.Core.Exceptions;

internal class OfferAccessException : InvestTrackerException
{
    public OfferAccessException(Guid offerId) : base($"Current user cannot get access to offer with ID: '{offerId}'.")
    {
    }
}