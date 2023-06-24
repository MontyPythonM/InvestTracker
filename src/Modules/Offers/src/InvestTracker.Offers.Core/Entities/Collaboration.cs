namespace InvestTracker.Offers.Core.Entities;

public class Collaboration
{
    public Guid Id { get; set; }
    public Advisor Advisor { get; set; }
    public Investor Investor { get; set; }
    public InvestmentStrategy? InvestmentStrategy { get; set; }
}