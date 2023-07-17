using InvestTracker.Offers.Core.Enums;

namespace InvestTracker.Offers.Core.Features.Invitations.Queries.GetInvitations;

public class InvitationDto
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public string SenderFullName { get; set; } = string.Empty;
    public Guid AdvisorId { get; set; }
    public string AdvisorFullName { get; set; } = string.Empty;
    public Guid OfferId { get; set; }
    public string OfferTitle { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
    public DateTime? StatusChangedAt { get; set; }
    public InvitationStatus Status { get; set; }
}