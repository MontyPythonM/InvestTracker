namespace InvestTracker.Offers.Core.Queries.GetOfferDetails.Dtos;

internal class OfferDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public AdvisorDetailsDto Advisor { get; set; }
    public List<string> Tags { get; set; }
}



