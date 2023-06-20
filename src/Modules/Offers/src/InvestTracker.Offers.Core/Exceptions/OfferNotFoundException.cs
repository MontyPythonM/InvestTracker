using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Offers.Core.Exceptions;

internal class OfferNotFoundException : InvestTrackerException
{
    public OfferNotFoundException(string message) : base(message)
    {
    }
}