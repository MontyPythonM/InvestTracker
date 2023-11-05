using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Separate_Amount_And_Volume_Transactions_And_TPC_Assets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "investment-strategies");

            migrationBuilder.DropTable(
                name: "Assets",
                schema: "investment-strategies");

            migrationBuilder.DropColumn(
                name: "Assets",
                schema: "investment-strategies",
                table: "Portfolios");

            migrationBuilder.CreateTable(
                name: "FinancialAssets.Cash",
                schema: "investment-strategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAssets.Cash", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialAssets.EdoTreasuryBonds",
                schema: "investment-strategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    FirstYearInterestRate = table.Column<decimal>(type: "numeric", nullable: false),
                    Margin = table.Column<decimal>(type: "numeric", nullable: false),
                    RedemptionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAssets.EdoTreasuryBonds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions.AmountTransaction",
                schema: "investment-strategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    CashId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions.AmountTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions.AmountTransaction_FinancialAssets.Cash_CashId",
                        column: x => x.CashId,
                        principalSchema: "investment-strategies",
                        principalTable: "FinancialAssets.Cash",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions.VolumeTransaction",
                schema: "investment-strategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Volume = table.Column<int>(type: "integer", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    EdoTreasuryBondId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions.VolumeTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions.VolumeTransaction_FinancialAssets.EdoTreasuryB~",
                        column: x => x.EdoTreasuryBondId,
                        principalSchema: "investment-strategies",
                        principalTable: "FinancialAssets.EdoTreasuryBonds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions.AmountTransaction_CashId",
                schema: "investment-strategies",
                table: "Transactions.AmountTransaction",
                column: "CashId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions.VolumeTransaction_EdoTreasuryBondId",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction",
                column: "EdoTreasuryBondId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions.AmountTransaction",
                schema: "investment-strategies");

            migrationBuilder.DropTable(
                name: "Transactions.VolumeTransaction",
                schema: "investment-strategies");

            migrationBuilder.DropTable(
                name: "FinancialAssets.Cash",
                schema: "investment-strategies");

            migrationBuilder.DropTable(
                name: "FinancialAssets.EdoTreasuryBonds",
                schema: "investment-strategies");

            migrationBuilder.AddColumn<string>(
                name: "Assets",
                schema: "investment-strategies",
                table: "Portfolios",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Assets",
                schema: "investment-strategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "investment-strategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    AssetId = table.Column<Guid>(type: "uuid", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Assets_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "investment-strategies",
                        principalTable: "Assets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AssetId",
                schema: "investment-strategies",
                table: "Transactions",
                column: "AssetId");
        }
    }
}
