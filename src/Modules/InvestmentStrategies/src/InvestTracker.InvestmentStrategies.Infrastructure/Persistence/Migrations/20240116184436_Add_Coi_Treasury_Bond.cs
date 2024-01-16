using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Coi_Treasury_Bond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoiTreasuryBondId",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FinancialAssets.CoiTreasuryBonds",
                schema: "investment-strategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    FirstYearInterestRate = table.Column<decimal>(type: "numeric", nullable: false),
                    Margin = table.Column<decimal>(type: "numeric", nullable: false),
                    PurchaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAssets.CoiTreasuryBonds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialAssets.CoiTreasuryBonds_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalSchema: "investment-strategies",
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions.VolumeTransaction_CoiTreasuryBondId",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction",
                column: "CoiTreasuryBondId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialAssets.CoiTreasuryBonds_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.CoiTreasuryBonds",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions.VolumeTransaction_FinancialAssets.CoiTreasuryB~",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction",
                column: "CoiTreasuryBondId",
                principalSchema: "investment-strategies",
                principalTable: "FinancialAssets.CoiTreasuryBonds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions.VolumeTransaction_FinancialAssets.CoiTreasuryB~",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction");

            migrationBuilder.DropTable(
                name: "FinancialAssets.CoiTreasuryBonds",
                schema: "investment-strategies");

            migrationBuilder.DropIndex(
                name: "IX_Transactions.VolumeTransaction_CoiTreasuryBondId",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction");

            migrationBuilder.DropColumn(
                name: "CoiTreasuryBondId",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction");
        }
    }
}
