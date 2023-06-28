using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.Offers.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Invitation_Remove_Investment_Strategies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborations_InvestmentStrategies_InvestmentStrategyId",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.DropTable(
                name: "InvestmentStrategies",
                schema: "offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collaborations",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.DropIndex(
                name: "IX_Collaborations_AdvisorId",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.DropIndex(
                name: "IX_Collaborations_InvestmentStrategyId",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.DropColumn(
                name: "InvestmentStrategyId",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledAt",
                schema: "offers",
                table: "Collaborations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "offers",
                table: "Collaborations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                schema: "offers",
                table: "Collaborations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collaborations",
                schema: "offers",
                table: "Collaborations",
                columns: new[] { "AdvisorId", "InvestorId" });

            migrationBuilder.CreateTable(
                name: "Invitations",
                schema: "offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    OfferId = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StatusChangedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Investors_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "offers",
                        principalTable: "Investors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Invitations_Offers_OfferId",
                        column: x => x.OfferId,
                        principalSchema: "offers",
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_OfferId",
                schema: "offers",
                table: "Invitations",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_SenderId",
                schema: "offers",
                table: "Invitations",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitations",
                schema: "offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collaborations",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.DropColumn(
                name: "CancelledAt",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "offers",
                table: "Collaborations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InvestmentStrategyId",
                schema: "offers",
                table: "Collaborations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collaborations",
                schema: "offers",
                table: "Collaborations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InvestmentStrategies",
                schema: "offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentStrategies", x => x.Id);
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

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborations_InvestmentStrategies_InvestmentStrategyId",
                schema: "offers",
                table: "Collaborations",
                column: "InvestmentStrategyId",
                principalSchema: "offers",
                principalTable: "InvestmentStrategies",
                principalColumn: "Id");
        }
    }
}
