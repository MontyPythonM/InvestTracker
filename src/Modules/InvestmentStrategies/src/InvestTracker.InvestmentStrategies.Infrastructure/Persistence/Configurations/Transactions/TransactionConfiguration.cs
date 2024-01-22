using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Configurations.Transactions;

internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");
        
        builder.ComplexProperty(asset => asset.Id)
            .IsRequired();

        builder.ComplexProperty(asset => asset.Amount)
            .IsRequired();
        
        builder.ComplexProperty(asset => asset.TransactionDate)
            .IsRequired();

        builder.ComplexProperty(asset => asset.Note);
        
        builder
            .HasDiscriminator<string>("Type")
            .HasValue<IncomingTransaction>(nameof(IncomingTransaction))
            .HasValue<OutgoingTransaction>(nameof(OutgoingTransaction));
    }
}