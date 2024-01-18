using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Use_FinancialAsset_In_Portfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions.AmountTransaction_FinancialAssets.Cash_CashId",
                schema: "investment-strategies",
                table: "Transactions.AmountTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions.VolumeTransaction_FinancialAssets.CoiTreasuryB~",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions.VolumeTransaction_FinancialAssets.EdoTreasuryB~",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction");

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
                newName: "FinancialAsset.EdoTreasuryBond",
                newSchema: "investment-strategies");

            migrationBuilder.RenameTable(
                name: "FinancialAssets.CoiTreasuryBonds",
                schema: "investment-strategies",
                newName: "FinancialAsset.CoiTreasuryBond",
                newSchema: "investment-strategies");

            migrationBuilder.RenameTable(
                name: "FinancialAssets.Cash",
                schema: "investment-strategies",
                newName: "FinancialAsset.Cash",
                newSchema: "investment-strategies");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAssets.EdoTreasuryBonds_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBond",
                newName: "IX_FinancialAsset.EdoTreasuryBond_PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAssets.CoiTreasuryBonds_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBond",
                newName: "IX_FinancialAsset.CoiTreasuryBond_PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAssets.Cash_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.Cash",
                newName: "IX_FinancialAsset.Cash_PortfolioId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions.AmountTransaction_FinancialAsset.Cash_CashId",
                schema: "investment-strategies",
                table: "Transactions.AmountTransaction",
                column: "CashId",
                principalSchema: "investment-strategies",
                principalTable: "FinancialAsset.Cash",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions.VolumeTransaction_FinancialAsset.CoiTreasuryBo~",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction",
                column: "CoiTreasuryBondId",
                principalSchema: "investment-strategies",
                principalTable: "FinancialAsset.CoiTreasuryBond",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions.VolumeTransaction_FinancialAsset.EdoTreasuryBo~",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction",
                column: "EdoTreasuryBondId",
                principalSchema: "investment-strategies",
                principalTable: "FinancialAsset.EdoTreasuryBond",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAsset.Cash_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.Cash");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAsset.CoiTreasuryBond_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBond");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialAsset.EdoTreasuryBond_Portfolios_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBond");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions.AmountTransaction_FinancialAsset.Cash_CashId",
                schema: "investment-strategies",
                table: "Transactions.AmountTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions.VolumeTransaction_FinancialAsset.CoiTreasuryBo~",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions.VolumeTransaction_FinancialAsset.EdoTreasuryBo~",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAsset.EdoTreasuryBond",
                schema: "investment-strategies",
                table: "FinancialAsset.EdoTreasuryBond");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAsset.CoiTreasuryBond",
                schema: "investment-strategies",
                table: "FinancialAsset.CoiTreasuryBond");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAsset.Cash",
                schema: "investment-strategies",
                table: "FinancialAsset.Cash");

            migrationBuilder.RenameTable(
                name: "FinancialAsset.EdoTreasuryBond",
                schema: "investment-strategies",
                newName: "FinancialAssets.EdoTreasuryBonds",
                newSchema: "investment-strategies");

            migrationBuilder.RenameTable(
                name: "FinancialAsset.CoiTreasuryBond",
                schema: "investment-strategies",
                newName: "FinancialAssets.CoiTreasuryBonds",
                newSchema: "investment-strategies");

            migrationBuilder.RenameTable(
                name: "FinancialAsset.Cash",
                schema: "investment-strategies",
                newName: "FinancialAssets.Cash",
                newSchema: "investment-strategies");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAsset.EdoTreasuryBond_PortfolioId",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                newName: "IX_FinancialAssets.EdoTreasuryBonds_PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialAsset.CoiTreasuryBond_PortfolioId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions.AmountTransaction_FinancialAssets.Cash_CashId",
                schema: "investment-strategies",
                table: "Transactions.AmountTransaction",
                column: "CashId",
                principalSchema: "investment-strategies",
                principalTable: "FinancialAssets.Cash",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions.VolumeTransaction_FinancialAssets.CoiTreasuryB~",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction",
                column: "CoiTreasuryBondId",
                principalSchema: "investment-strategies",
                principalTable: "FinancialAssets.CoiTreasuryBonds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions.VolumeTransaction_FinancialAssets.EdoTreasuryB~",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction",
                column: "EdoTreasuryBondId",
                principalSchema: "investment-strategies",
                principalTable: "FinancialAssets.EdoTreasuryBonds",
                principalColumn: "Id");
        }
    }
}
