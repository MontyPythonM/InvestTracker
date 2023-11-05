using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Transactions.Volume;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations.Transactions;

internal class VolumeTransactionConfiguration : IEntityTypeConfiguration<VolumeTransaction>
{
    public void Configure(EntityTypeBuilder<VolumeTransaction> builder)
    {
        builder.ToTable("Transactions.VolumeTransaction");

        builder.Property(transaction => transaction.Id)
            .IsRequired()
            .HasConversion(t => t.Value, t => new TransactionId(t));
        
        builder.Property(transaction => transaction.Volume)
            .IsRequired()
            .HasConversion(t => t.Value, t => new Volume(t));

        builder.Property(transaction => transaction.TransactionDate)
            .IsRequired();
        
        builder.Property(transaction => transaction.Note)
            .HasConversion(t => t.Value, t => new Note(t));
        
        builder
            .HasDiscriminator<string>("Type")
            .HasValue<IncomingVolumeTransaction>(nameof(IncomingVolumeTransaction))
            .HasValue<OutgoingVolumeTransaction>(nameof(OutgoingVolumeTransaction));
    }
}