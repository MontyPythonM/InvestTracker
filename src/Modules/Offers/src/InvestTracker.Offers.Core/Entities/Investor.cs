namespace InvestTracker.Offers.Core.Entities;

public class Investor
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Collaboration> Collaborations { get; set; } = new List<Collaboration>();
}