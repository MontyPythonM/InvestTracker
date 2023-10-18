using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.Offers.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Id_In_Collaboration_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Collaborations",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.DropIndex(
                name: "IX_Collaborations_InvestorId",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "offers",
                table: "Offers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "offers",
                table: "Offers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StatusChangedAt",
                schema: "offers",
                table: "Invitations",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentAt",
                schema: "offers",
                table: "Invitations",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "offers",
                table: "Collaborations",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CancelledAt",
                schema: "offers",
                table: "Collaborations",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "offers",
                table: "Collaborations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collaborations",
                schema: "offers",
                table: "Collaborations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborations_AdvisorId",
                schema: "offers",
                table: "Collaborations",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborations_InvestorId_AdvisorId",
                schema: "offers",
                table: "Collaborations",
                columns: new[] { "InvestorId", "AdvisorId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Collaborations",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.DropIndex(
                name: "IX_Collaborations_AdvisorId",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.DropIndex(
                name: "IX_Collaborations_InvestorId_AdvisorId",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "offers",
                table: "Collaborations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "offers",
                table: "Offers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "offers",
                table: "Offers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StatusChangedAt",
                schema: "offers",
                table: "Invitations",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentAt",
                schema: "offers",
                table: "Invitations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "offers",
                table: "Collaborations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CancelledAt",
                schema: "offers",
                table: "Collaborations",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collaborations",
                schema: "offers",
                table: "Collaborations",
                columns: new[] { "AdvisorId", "InvestorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborations_InvestorId",
                schema: "offers",
                table: "Collaborations",
                column: "InvestorId");
        }
    }
}
