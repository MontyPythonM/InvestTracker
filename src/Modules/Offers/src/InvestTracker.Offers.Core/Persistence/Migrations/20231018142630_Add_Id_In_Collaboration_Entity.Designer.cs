﻿// <auto-generated />
using System;
using InvestTracker.Offers.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InvestTracker.Offers.Core.Persistence.Migrations
{
    [DbContext(typeof(OffersDbContext))]
    [Migration("20231018142630_Add_Id_In_Collaboration_Entity")]
    partial class Add_Id_In_Collaboration_Entity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("offers")
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InvestTracker.Offers.Core.Entities.Advisor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .HasMaxLength(3000)
                        .HasColumnType("character varying(3000)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Advisors", "offers");
                });

            modelBuilder.Entity("InvestTracker.Offers.Core.Entities.Collaboration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdvisorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CancelledAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("InvestorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AdvisorId");

                    b.HasIndex("InvestorId", "AdvisorId");

                    b.ToTable("Collaborations", "offers");
                });

            modelBuilder.Entity("InvestTracker.Offers.Core.Entities.Investor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Investors", "offers");
                });

            modelBuilder.Entity("InvestTracker.Offers.Core.Entities.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OfferId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("StatusChangedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.HasIndex("SenderId");

                    b.ToTable("Invitations", "offers");
                });

            modelBuilder.Entity("InvestTracker.Offers.Core.Entities.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdvisorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(3000)
                        .HasColumnType("character varying(3000)");

                    b.Property<decimal?>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AdvisorId");

                    b.ToTable("Offers", "offers");
                });

            modelBuilder.Entity("InvestTracker.Offers.Core.Entities.Collaboration", b =>
                {
                    b.HasOne("InvestTracker.Offers.Core.Entities.Advisor", "Advisor")
                        .WithMany("Collaborations")
                        .HasForeignKey("AdvisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestTracker.Offers.Core.Entities.Investor", "Investor")
                        .WithMany("Collaborations")
                        .HasForeignKey("InvestorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Advisor");

                    b.Navigation("Investor");
                });

            modelBuilder.Entity("InvestTracker.Offers.Core.Entities.Invitation", b =>
                {
                    b.HasOne("InvestTracker.Offers.Core.Entities.Offer", "Offer")
                        .WithMany("Invitations")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestTracker.Offers.Core.Entities.Investor", "Sender")
                        .WithMany("Invitations")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("InvestTracker.Offers.Core.Entities.Offer", b =>
                {
                    b.HasOne("InvestTracker.Offers.Core.Entities.Advisor", "Advisor")
                        .WithMany("Offers")
                        .HasForeignKey("AdvisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Advisor");
                });

            modelBuilder.Entity("InvestTracker.Offers.Core.Entities.Advisor", b =>
                {
                    b.Navigation("Collaborations");

                    b.Navigation("Offers");
                });

            modelBuilder.Entity("InvestTracker.Offers.Core.Entities.Investor", b =>
                {
                    b.Navigation("Collaborations");

                    b.Navigation("Invitations");
                });

            modelBuilder.Entity("InvestTracker.Offers.Core.Entities.Offer", b =>
                {
                    b.Navigation("Invitations");
                });
#pragma warning restore 612, 618
        }
    }
}