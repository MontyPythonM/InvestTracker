namespace InvestTracker.Offers.Core.Entities;

public class Collaboration
{
    public Guid Id { get; set; }
    public Guid AdvisorId { get; set; }
    public Guid InvestorId { get; set; }
    public virtual Advisor Advisor { get; set; }
    public virtual Investor Investor { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public bool IsCancelled { get; set; }
}