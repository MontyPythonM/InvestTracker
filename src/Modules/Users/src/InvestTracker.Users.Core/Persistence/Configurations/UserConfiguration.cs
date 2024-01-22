using InvestTracker.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Role = InvestTracker.Users.Core.Entities.Role;
using Subscription = InvestTracker.Users.Core.Entities.Subscription;

namespace InvestTracker.Users.Core.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Email)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(u => u.Password)
            .IsRequired();

        builder.Property(u => u.FullName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(u => u.Phone)
            .HasMaxLength(20);
        
        builder.OwnsOne<Role>(user => user.Role);
        builder.OwnsOne<Subscription>(user => user.Subscription);
    }
}