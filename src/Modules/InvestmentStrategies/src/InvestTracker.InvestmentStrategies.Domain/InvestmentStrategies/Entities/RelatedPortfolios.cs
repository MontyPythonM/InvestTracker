namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;

public class RelatedPortfolios
{
    public Guid PortfolioId { get; private set; }

    private RelatedPortfolios()
    {
    }

    internal RelatedPortfolios(Guid portfolioId)
    {
        PortfolioId = portfolioId;
    }
}