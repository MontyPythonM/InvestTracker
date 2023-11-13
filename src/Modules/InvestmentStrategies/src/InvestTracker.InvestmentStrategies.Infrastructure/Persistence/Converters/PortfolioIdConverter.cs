using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Converters;

internal sealed class PortfolioIdConverter : ValueConverter<PortfolioId, Guid>
{
    public PortfolioIdConverter() : base(s => s.Value, s => new PortfolioId(s))
    {
    }
}