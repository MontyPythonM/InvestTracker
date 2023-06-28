namespace InvestTracker.Offers.Core.Features.Offers.Queries.GetOffers;

internal class OfferDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string AdvisorFullName { get; set; } = string.Empty;
}