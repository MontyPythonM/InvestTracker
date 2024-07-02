using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Advisors.Commands.UpdateAdvisor;

internal record UpdateAdvisor(Guid AdvisorId, string? PhoneNumber, string? Bio, string? CompanyName, string? Avatar) : ICommand;