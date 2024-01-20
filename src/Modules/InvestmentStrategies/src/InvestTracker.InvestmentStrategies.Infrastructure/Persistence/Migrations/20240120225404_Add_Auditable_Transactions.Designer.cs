﻿// <auto-generated />
using System;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(InvestmentStrategiesDbContext))]
    [Migration("20240120225404_Add_Auditable_Transactions")]
    partial class Add_Auditable_Transactions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("PrincipalId", "AdvisorId");

                    b.ToTable("Collaborations", "investment-strategies");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities.InvestmentStrategy", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsShareEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

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

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions.FinancialAsset", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Portfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("InvestmentStrategyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
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

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FinancialAssetId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
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

                    b.HasIndex("FinancialAssetId");

                    b.ToTable("Transactions", "investment-strategies");

                    b.HasDiscriminator<string>("Type").HasValue("Transaction");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities.Stakeholder", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

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

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Entities.ExchangeRateEntity", b =>
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

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Entities.InflationRateEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Currency")
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

                    b.Property<DateOnly>("MonthlyDate")
                        .HasColumnType("date");

                    b.Property<decimal>("Value")
                        .HasPrecision(12, 4)
                        .HasColumnType("numeric(12,4)");

                    b.HasKey("Id");

                    b.HasIndex("Currency");

                    b.HasIndex("MonthlyDate");

                    b.ToTable("InflationRates", "investment-strategies");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets.Cash", b =>
                {
                    b.HasBaseType("InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions.FinancialAsset");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("FinancialAssets.Cash", "investment-strategies");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets.CoiTreasuryBond", b =>
                {
                    b.HasBaseType("InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions.FinancialAsset");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
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

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("PurchaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("FinancialAssets.CoiTreasuryBonds", "investment-strategies");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets.EdoTreasuryBond", b =>
                {
                    b.HasBaseType("InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions.FinancialAsset");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
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

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("PurchaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("FinancialAssets.EdoTreasuryBonds", "investment-strategies");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.IncomingTransaction", b =>
                {
                    b.HasBaseType("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Transaction");

                    b.HasDiscriminator().HasValue("IncomingTransaction");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.OutgoingTransaction", b =>
                {
                    b.HasBaseType("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Transaction");

                    b.HasDiscriminator().HasValue("OutgoingTransaction");
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

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions.FinancialAsset", b =>
                {
                    b.HasOne("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Portfolio", null)
                        .WithMany("FinancialAssets")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Transaction", b =>
                {
                    b.HasOne("InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions.FinancialAsset", null)
                        .WithMany("Transactions")
                        .HasForeignKey("FinancialAssetId");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions.FinancialAsset", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Portfolio", b =>
                {
                    b.Navigation("FinancialAssets");
                });
#pragma warning restore 612, 618
        }
    }
}
