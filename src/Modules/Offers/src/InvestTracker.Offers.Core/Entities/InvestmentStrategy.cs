namespace InvestTracker.Offers.Core.Entities;

public class InvestmentStrategy
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid OwnerId { get; set; }
    public ICollection<Collaboration> Collaborations { get; set; } = new List<Collaboration>();
}