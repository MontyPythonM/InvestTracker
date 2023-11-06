using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Financial_Assets_Indirect_Relation_In_Portfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FinancialAssets",
                schema: "investment-strategies",
                table: "Portfolios",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinancialAssets",
                schema: "investment-strategies",
                table: "Portfolios");
        }
    }
}
