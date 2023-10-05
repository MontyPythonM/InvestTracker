using InvestTracker.InvestmentStrategies.Domain.Asset.Entities;
using InvestTracker.InvestmentStrategies.Domain.Asset.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations;

internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(transaction => transaction.Id)
            .IsRequired()
            .HasConversion(t => t.Value, t => new TransactionId(t));
        
        builder.Property(transaction => transaction.Amount)
            .IsRequired()
            .HasConversion(t => t.Value, t => new Amount(t));

        builder.Property(transaction => transaction.TransactionDate)
            .IsRequired();
        
        builder.Property(transaction => transaction.Note)
            .HasConversion(t => t.Value, t => new Note(t));
        
        builder
            .HasDiscriminator<string>("Type")
            .HasValue<IncomingTransaction>(nameof(IncomingTransaction))
            .HasValue<OutgoingTransaction>(nameof(OutgoingTransaction));
    }
}