namespace InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaborations;

internal class CollaborationDto
{
    public Guid Id { get; set; }
    public Guid AdvisorId { get; set; }
    public Guid InvestorId { get; set; }
    public string AdvisorFullName { get; set; } = string.Empty;
    public string InvestorFullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public bool IsCancelled { get; set; }
}