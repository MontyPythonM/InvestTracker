using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Use_PurchaseDate_Instead_Of_RedemptionDate_For_EDO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RedemptionDate",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                newName: "PurchaseDate");
            
            migrationBuilder.Sql(@"
                UPDATE ""investment-strategies"".""FinancialAssets.EdoTreasuryBonds""
                SET ""PurchaseDate"" = DATE_TRUNC('day', ""PurchaseDate"" - INTERVAL '10 years')
                WHERE 1 = 1;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PurchaseDate",
                schema: "investment-strategies",
                table: "FinancialAssets.EdoTreasuryBonds",
                newName: "RedemptionDate");
            
            migrationBuilder.Sql(@"
                UPDATE ""investment-strategies"".""FinancialAssets.EdoTreasuryBonds""
                SET ""RedemptionDate"" = DATE_TRUNC('day', ""RedemptionDate"" + INTERVAL '10 years')
                WHERE 1 = 1;
            ");
        }
    }
}
