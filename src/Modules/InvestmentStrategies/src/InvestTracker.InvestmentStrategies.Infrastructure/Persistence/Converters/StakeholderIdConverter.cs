using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Converters;

internal sealed class StakeholderIdConverter : ValueConverter<StakeholderId, Guid>
{
    public StakeholderIdConverter() : base(s => s.Value, s => new StakeholderId(s))
    {
    }
}