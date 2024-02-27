using InvestTracker.Notifications.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.Notifications.Infrastructure.Persistence.Configurations;

internal class GlobalSettingsConfiguration : IEntityTypeConfiguration<GlobalSettings>
{
    public void Configure(EntityTypeBuilder<GlobalSettings> builder)
    {
        builder.HasData(GlobalSettings.CreateInitialSetup());
    }
}