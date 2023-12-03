using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Reference_To_Portfolio_In_Assets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAssets.Cash_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAssets.EdoTreasuryBonds_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAssets.Cash_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash",
                column: "PortfolioId",
                principalSchema: "investment-strategies",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAssets.EdoTreasuryBonds_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                column: "PortfolioId",
                principalSchema: "investment-strategies",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAssets.Cash_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAssets.EdoTreasuryBonds_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAssets.Cash_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash",
                column: "PortfolioId",
                principalSchema: "investment-strategies",
                principalTable: "Portfolios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAssets.EdoTreasuryBonds_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                column: "PortfolioId",
                principalSchema: "investment-strategies",
                principalTable: "Portfolios",
                principalColumn: "Id");
        }
    }
}
