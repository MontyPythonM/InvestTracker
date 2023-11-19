using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Auditable_Interceptor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "investment-strategies",
                table: "Stakeholders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "investment-strategies",
                table: "Stakeholders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "investment-strategies",
                table: "Stakeholders",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                schema: "investment-strategies",
                table: "Stakeholders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "investment-strategies",
                table: "Portfolios",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "investment-strategies",
                table: "Portfolios",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "investment-strategies",
                table: "Portfolios",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                schema: "investment-strategies",
                table: "Portfolios",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "investment-strategies",
                table: "InvestmentStrategies",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "investment-strategies",
                table: "InvestmentStrategies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "investment-strategies",
                table: "InvestmentStrategies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                schema: "investment-strategies",
                table: "InvestmentStrategies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "investment-strategies",
                table: "Collaborations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "investment-strategies",
                table: "Collaborations",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                schema: "investment-strategies",
                table: "Collaborations",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "investment-strategies",
                table: "Stakeholders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "investment-strategies",
                table: "Stakeholders");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "investment-strategies",
                table: "Stakeholders");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "investment-strategies",
                table: "Stakeholders");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "investment-strategies",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "investment-strategies",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "investment-strategies",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "investment-strategies",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "investment-strategies",
                table: "InvestmentStrategies");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "investment-strategies",
                table: "InvestmentStrategies");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "investment-strategies",
                table: "InvestmentStrategies");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "investment-strategies",
                table: "InvestmentStrategies");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "investment-strategies",
                table: "Collaborations");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "investment-strategies",
                table: "Collaborations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "investment-strategies",
                table: "Collaborations");
        }
    }
}
