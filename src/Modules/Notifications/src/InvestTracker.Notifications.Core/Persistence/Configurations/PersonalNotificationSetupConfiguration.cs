using InvestTracker.Notifications.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.Notifications.Core.Persistence.Configurations;

internal class PersonalNotificationSetupConfiguration : IEntityTypeConfiguration<PersonalNotificationSetup>
{
    public void Configure(EntityTypeBuilder<PersonalNotificationSetup> builder)
    {
        throw new NotImplementedException();
    }
}