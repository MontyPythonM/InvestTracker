using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Financial_Asset_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAsset.CoiTreasuryBond_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBond");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAsset.EdoTreasuryBond_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBond");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAsset.EdoTreasuryBond",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBond");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAsset.CoiTreasuryBond",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBond");

            migrationBuilder.RenameTable(
                name: "FinancialAsset.EdoTreasuryBond",
                schema: "investment-strategies",
                newName: "FinancialAsset.EdoTreasuryBonds",
                newSchema: "investment-strategies");

            migrationBuilder.RenameTable(
                name: "FinancialAsset.CoiTreasuryBond",
                schema: "investment-strategies",
                newName: "FinancialAsset.CoiTreasuryBonds",
                newSchema: "investment-strategies");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAsset.EdoTreasuryBond_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBonds",
                newName: "IX_FinancialAsset.EdoTreasuryBonds_PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAsset.CoiTreasuryBond_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBonds",
                newName: "IX_FinancialAsset.CoiTreasuryBonds_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialAsset.EdoTreasuryBonds",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBonds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialAsset.CoiTreasuryBonds",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBonds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAsset.CoiTreasuryBonds_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBonds",
                column: "PortfolioId",
                principalSchema: "investment-strategies",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAsset.EdoTreasuryBonds_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBonds",
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
                name: "FK_FinancialAsset.CoiTreasuryBonds_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBonds");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAsset.EdoTreasuryBonds_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBonds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAsset.EdoTreasuryBonds",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBonds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAsset.CoiTreasuryBonds",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBonds");

            migrationBuilder.RenameTable(
                name: "FinancialAsset.EdoTreasuryBonds",
                schema: "investment-strategies",
                newName: "FinancialAsset.EdoTreasuryBond",
                newSchema: "investment-strategies");

            migrationBuilder.RenameTable(
                name: "FinancialAsset.CoiTreasuryBonds",
                schema: "investment-strategies",
                newName: "FinancialAsset.CoiTreasuryBond",
                newSchema: "investment-strategies");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAsset.EdoTreasuryBonds_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBond",
                newName: "IX_FinancialAsset.EdoTreasuryBond_PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAsset.CoiTreasuryBonds_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBond",
                newName: "IX_FinancialAsset.CoiTreasuryBond_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialAsset.EdoTreasuryBond",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBond",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialAsset.CoiTreasuryBond",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBond",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAsset.CoiTreasuryBond_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBond",
                column: "PortfolioId",
                principalSchema: "investment-strategies",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAsset.EdoTreasuryBond_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBond",
                column: "PortfolioId",
                principalSchema: "investment-strategies",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
