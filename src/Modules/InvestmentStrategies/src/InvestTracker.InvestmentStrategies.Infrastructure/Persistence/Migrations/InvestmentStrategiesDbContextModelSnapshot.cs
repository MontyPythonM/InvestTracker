﻿// <auto-generated />
using System;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(InvestmentStrategiesDbContext))]
    partial class InvestmentStrategiesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("investment-strategies")
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities.Collaboration", b =>
                {
                    b.Property<Guid>("PrincipalId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdvisorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("PrincipalId", "AdvisorId");

                    b.ToTable("Collaborations", "investment-strategies");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.InvestmentStrategy", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsShareEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Owner")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("InvestmentStrategies", "investment-strategies");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets.Cash", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PortfolioId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("FinancialAssets.Cash", "investment-strategies");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets.EdoTreasuryBond", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("FirstYearInterestRate")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Margin")
                        .HasColumnType("numeric");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PortfolioId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("RedemptionDate")
                        .HasColumnType("date");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("FinancialAssets.EdoTreasuryBonds", "investment-strategies");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Portfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("InvestmentStrategyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Portfolios", "investment-strategies");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.AmountTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("CashId")
                        .HasColumnType("uuid");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CashId");

                    b.ToTable("Transactions.AmountTransaction", "investment-strategies");

                    b.HasDiscriminator<string>("Type").HasValue("AmountTransaction");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.VolumeTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EdoTreasuryBondId")
                        .HasColumnType("uuid");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Volume")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EdoTreasuryBondId");

                    b.ToTable("Transactions.VolumeTransaction", "investment-strategies");

                    b.HasDiscriminator<string>("Type").HasValue("VolumeTransaction");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities.Stakeholder", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subscription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Stakeholders", "investment-strategies");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.ExchangeRate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ImportedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Metadata")
                        .HasMaxLength(1500)
                        .HasColumnType("character varying(1500)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Value")
                        .HasPrecision(12, 4)
                        .HasColumnType("numeric(12,4)");

                    b.HasKey("Id");

                    b.HasIndex("Date");

                    b.HasIndex("To");

                    b.ToTable("ExchangeRates", "investment-strategies");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Amount.IncomingAmountTransaction", b =>
                {
                    b.HasBaseType("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.AmountTransaction");

                    b.HasDiscriminator().HasValue("IncomingAmountTransaction");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Amount.OutgoingAmountTransaction", b =>
                {
                    b.HasBaseType("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.AmountTransaction");

                    b.HasDiscriminator().HasValue("OutgoingAmountTransaction");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Volume.IncomingVolumeTransaction", b =>
                {
                    b.HasBaseType("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.VolumeTransaction");

                    b.HasDiscriminator().HasValue("IncomingVolumeTransaction");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Volume.OutgoingVolumeTransaction", b =>
                {
                    b.HasBaseType("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.VolumeTransaction");

                    b.HasDiscriminator().HasValue("OutgoingVolumeTransaction");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.InvestmentStrategy", b =>
                {
                    b.OwnsMany("InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.RelatedCollaborators", "Collaborators", b1 =>
                        {
                            b1.Property<Guid>("InvestmentStrategyId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("CollaboratorId")
                                .HasColumnType("uuid");

                            b1.HasKey("InvestmentStrategyId", "Id");

                            b1.ToTable("RelatedCollaborators", "investment-strategies");

                            b1.WithOwner()
                                .HasForeignKey("InvestmentStrategyId");
                        });

                    b.OwnsMany("InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.RelatedPortfolios", "Portfolios", b1 =>
                        {
                            b1.Property<Guid>("InvestmentStrategyId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("PortfolioId")
                                .HasColumnType("uuid");

                            b1.HasKey("InvestmentStrategyId", "Id");

                            b1.ToTable("RelatedPortfolios", "investment-strategies");

                            b1.WithOwner()
                                .HasForeignKey("InvestmentStrategyId");
                        });

                    b.Navigation("Collaborators");

                    b.Navigation("Portfolios");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets.Cash", b =>
                {
                    b.HasOne("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Portfolio", null)
                        .WithMany("Cash")
                        .HasForeignKey("PortfolioId");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets.EdoTreasuryBond", b =>
                {
                    b.HasOne("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Portfolio", null)
                        .WithMany("EdoTreasuryBonds")
                        .HasForeignKey("PortfolioId");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.AmountTransaction", b =>
                {
                    b.HasOne("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets.Cash", null)
                        .WithMany("Transactions")
                        .HasForeignKey("CashId");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.VolumeTransaction", b =>
                {
                    b.HasOne("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets.EdoTreasuryBond", null)
                        .WithMany("Transactions")
                        .HasForeignKey("EdoTreasuryBondId");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets.Cash", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets.EdoTreasuryBond", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Portfolio", b =>
                {
                    b.Navigation("Cash");

                    b.Navigation("EdoTreasuryBonds");
                });
#pragma warning restore 612, 618
        }
    }
}
