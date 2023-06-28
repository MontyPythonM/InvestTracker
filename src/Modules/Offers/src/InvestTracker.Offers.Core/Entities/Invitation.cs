using InvestTracker.Offers.Core.Enums;

namespace InvestTracker.Offers.Core.Entities;

public class Invitation
{
    public Guid Id { get; set; }
    public virtual Investor Sender { get; set; }
    public Guid SenderId { get; set; }
    public virtual Offer Offer { get; set; }
    public Guid OfferId { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
    public DateTime? StatusChangedAt { get; set; }
    public InvitationStatus Status { get; set; } = InvitationStatus.Expected;
}