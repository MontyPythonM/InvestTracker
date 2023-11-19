namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Dto;

public class InvestmentStrategyDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public bool IsShareEnabled { get; set; }
    public IEnumerable<CollaboratorDto> Collaborators { get; set; } = new List<CollaboratorDto>();
    public IEnumerable<PortfolioDto> Portfolios { get; set; } = new List<PortfolioDto>();
}

public class CollaboratorDto
{
    public Guid CollaboratorId { get; set; }
    public string CollaboratorName { get; set; } = string.Empty;
}

public class PortfolioDto
{
    public Guid PortfolioId { get; set; }
    public string PortfolioTitle { get; set; } = string.Empty;
}