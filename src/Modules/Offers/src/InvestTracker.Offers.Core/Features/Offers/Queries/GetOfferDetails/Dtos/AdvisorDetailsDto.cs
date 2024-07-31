namespace InvestTracker.Offers.Core.Features.Offers.Queries.GetOfferDetails.Dtos;

internal class AdvisorDetailsDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Bio { get; set; }
    public string? CompanyName { get; set; }
    public string? Avatar { get; set; }
}