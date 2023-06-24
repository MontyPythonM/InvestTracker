using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.Offers.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Offers_Module_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "offers");

            migrationBuilder.CreateTable(
                name: "Advisors",
                schema: "offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Bio = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: true),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    AvatarUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advisors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentStrategies",
                schema: "offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentStrategies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investors",
                schema: "offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                schema: "offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: true),
                    Price = table.Column<decimal>(type: "numeric(2)", precision: 2, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AdvisorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalSchema: "offers",
                        principalTable: "Advisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Collaborations",
                schema: "offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdvisorId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvestorId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvestmentStrategyId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collaborations_Advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalSchema: "offers",
                        principalTable: "Advisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collaborations_InvestmentStrategies_InvestmentStrategyId",
                        column: x => x.InvestmentStrategyId,
                        principalSchema: "offers",
                        principalTable: "InvestmentStrategies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collaborations_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalSchema: "offers",
                        principalTable: "Investors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferTags",
                schema: "offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OfferId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferTags_Offers_OfferId",
                        column: x => x.OfferId,
                        principalSchema: "offers",
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborations_AdvisorId",
                schema: "offers",
                table: "Collaborations",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborations_InvestmentStrategyId",
                schema: "offers",
                table: "Collaborations",
                column: "InvestmentStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborations_InvestorId",
                schema: "offers",
                table: "Collaborations",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_AdvisorId",
                schema: "offers",
                table: "Offers",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferTags_OfferId",
                schema: "offers",
                table: "OfferTags",
                column: "OfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collaborations",
                schema: "offers");

            migrationBuilder.DropTable(
                name: "OfferTags",
                schema: "offers");

            migrationBuilder.DropTable(
                name: "InvestmentStrategies",
                schema: "offers");

            migrationBuilder.DropTable(
                name: "Investors",
                schema: "offers");

            migrationBuilder.DropTable(
                name: "Offers",
                schema: "offers");

            migrationBuilder.DropTable(
                name: "Advisors",
                schema: "offers");
        }
    }
}
