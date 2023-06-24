namespace InvestTracker.Offers.Core.Entities;

public class OfferTag
{
    public Guid Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public Offer Offer { get; set; }
}