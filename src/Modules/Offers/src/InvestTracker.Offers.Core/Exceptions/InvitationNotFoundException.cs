using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Offers.Core.Exceptions;

public class InvitationNotFoundException : InvestTrackerException
{
    public InvitationNotFoundException(Guid id) : base($"Invitation with ID: {id} not found.")
    {
    }
}