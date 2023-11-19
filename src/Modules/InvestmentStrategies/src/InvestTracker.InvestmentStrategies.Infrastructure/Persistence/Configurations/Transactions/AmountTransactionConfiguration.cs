using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Amount;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations.Transactions;

internal class AmountTransactionConfiguration : IEntityTypeConfiguration<AmountTransaction>
{
    public void Configure(EntityTypeBuilder<AmountTransaction> builder)
    {
        builder.ToTable("Transactions.AmountTransaction");

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
            .HasValue<IncomingAmountTransaction>(nameof(IncomingAmountTransaction))
            .HasValue<OutgoingAmountTransaction>(nameof(OutgoingAmountTransaction));
    }
}