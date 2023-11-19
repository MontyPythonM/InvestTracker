using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Converters;

internal sealed class InvestmentStrategyIdConverter : ValueConverter<InvestmentStrategyId, Guid>
{
    public InvestmentStrategyIdConverter() : base(s => s.Value, s => new InvestmentStrategyId(s))
    {
    }
}