namespace InvestTracker.Offers.Api.Dtos;

public record UpdateAdvisorDto(Guid Id, string? PhoneNumber, string? Bio, string? CompanyName, string? Avatar);