using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Converters;

internal sealed class FinancialAssetIdConverter : ValueConverter<FinancialAssetId, Guid>
{
    public FinancialAssetIdConverter() : base(c => c.Value, c => new FinancialAssetId(c))
    {
    }
}