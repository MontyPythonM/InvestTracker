using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAsset.Cash_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.Cash");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAsset.Cash",
                schema: "investment-strategies",
                table: "FinancialAsset.Cash");

            migrationBuilder.RenameTable(
                name: "FinancialAsset.EdoTreasuryBonds",
                schema: "investment-strategies",
                newName: "FinancialAssets.EdoTreasuryBonds",
                newSchema: "investment-strategies");

            migrationBuilder.RenameTable(
                name: "FinancialAsset.CoiTreasuryBonds",
                schema: "investment-strategies",
                newName: "FinancialAssets.CoiTreasuryBonds",
                newSchema: "investment-strategies");

            migrationBuilder.RenameTable(
                name: "FinancialAsset.Cash",
                schema: "investment-strategies",
                newName: "FinancialAssets.Cash",
                newSchema: "investment-strategies");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAsset.EdoTreasuryBonds_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                newName: "IX_FinancialAssets.EdoTreasuryBonds_PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAsset.CoiTreasuryBonds_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.CoiTreasuryBonds",
                newName: "IX_FinancialAssets.CoiTreasuryBonds_PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAsset.Cash_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash",
                newName: "IX_FinancialAssets.Cash_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialAssets.EdoTreasuryBonds",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialAssets.CoiTreasuryBonds",
                schema: "investment-strategies",
                table: "FinancialAssets.CoiTreasuryBonds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialAssets.Cash",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash",
                column: "Id");

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
                name: "FK_FinancialAssets.CoiTreasuryBonds_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.CoiTreasuryBonds",
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
                name: "FK_FinancialAssets.CoiTreasuryBonds_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.CoiTreasuryBonds");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAssets.EdoTreasuryBonds_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAssets.EdoTreasuryBonds",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAssets.CoiTreasuryBonds",
                schema: "investment-strategies",
                table: "FinancialAssets.CoiTreasuryBonds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAssets.Cash",
                schema: "investment-strategies",
                table: "FinancialAssets.Cash");

            migrationBuilder.RenameTable(
                name: "FinancialAssets.EdoTreasuryBonds",
                schema: "investment-strategies",
                newName: "FinancialAsset.EdoTreasuryBonds",
                newSchema: "investment-strategies");

            migrationBuilder.RenameTable(
                name: "FinancialAssets.CoiTreasuryBonds",
                schema: "investment-strategies",
                newName: "FinancialAsset.CoiTreasuryBonds",
                newSchema: "investment-strategies");

            migrationBuilder.RenameTable(
                name: "FinancialAssets.Cash",
                schema: "investment-strategies",
                newName: "FinancialAsset.Cash",
                newSchema: "investment-strategies");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAssets.EdoTreasuryBonds_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBonds",
                newName: "IX_FinancialAsset.EdoTreasuryBonds_PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAssets.CoiTreasuryBonds_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBonds",
                newName: "IX_FinancialAsset.CoiTreasuryBonds_PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAssets.Cash_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.Cash",
                newName: "IX_FinancialAsset.Cash_PortfolioId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialAsset.Cash",
                schema: "investment-strategies",
                table: "FinancialAsset.Cash",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialAsset.Cash_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.Cash",
                column: "PortfolioId",
                principalSchema: "investment-strategies",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
