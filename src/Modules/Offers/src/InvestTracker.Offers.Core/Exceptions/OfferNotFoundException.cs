using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Offers.Core.Exceptions;

internal class OfferNotFoundException : InvestTrackerException
{
    public OfferNotFoundException(Guid offerId) : base($"Offer with ID: {offerId} not found.")
    {
    }
}