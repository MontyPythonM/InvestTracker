using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Converters;

internal sealed class AssetIdConverter : ValueConverter<AssetId, Guid>
{
    public AssetIdConverter() : base(c => c.Value, c => new AssetId(c))
    {
    }
}