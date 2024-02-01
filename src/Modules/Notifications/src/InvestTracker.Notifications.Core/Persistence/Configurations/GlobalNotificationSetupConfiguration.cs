using InvestTracker.Notifications.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.Notifications.Core.Persistence.Configurations;

internal class GlobalNotificationSetupConfiguration : IEntityTypeConfiguration<GlobalNotificationSetup>
{
    public void Configure(EntityTypeBuilder<GlobalNotificationSetup> builder)
    {
        builder.HasData(GlobalNotificationSetup.CreateInitialSetup());
    }
}