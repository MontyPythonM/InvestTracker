using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Separate_Portfolios_From_Investment_Strategy_As_New_Aggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_InvestmentStrategies_InvestmentStrategyId",
                schema: "investment-strategies",
                table: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_InvestmentStrategyId",
                schema: "investment-strategies",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "FinancialAssets",
                schema: "investment-strategies",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvestmentStrategyId",
                schema: "investment-strategies",
                table: "Portfolios",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                schema: "investment-strategies",
                table: "Portfolios",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Portfolios",
                schema: "investment-strategies",
                table: "InvestmentStrategies",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAssets.EdoTreasuryBonds_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAssets.Cash_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash",
                column: "PortfolioId");

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

            migrationBuilder.DropIndex(
                name: "IX_FinancialAssets.EdoTreasuryBonds_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds");

            migrationBuilder.DropIndex(
                name: "IX_FinancialAssets.Cash_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "investment-strategies",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Portfolios",
                schema: "investment-strategies",
                table: "InvestmentStrategies");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvestmentStrategyId",
                schema: "investment-strategies",
                table: "Portfolios",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "FinancialAssets",
                schema: "investment-strategies",
                table: "Portfolios",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<int>(
                name: "Version",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "Version",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_InvestmentStrategyId",
                schema: "investment-strategies",
                table: "Portfolios",
                column: "InvestmentStrategyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_InvestmentStrategies_InvestmentStrategyId",
                schema: "investment-strategies",
                table: "Portfolios",
                column: "InvestmentStrategyId",
                principalSchema: "investment-strategies",
                principalTable: "InvestmentStrategies",
                principalColumn: "Id");
        }
    }
}
