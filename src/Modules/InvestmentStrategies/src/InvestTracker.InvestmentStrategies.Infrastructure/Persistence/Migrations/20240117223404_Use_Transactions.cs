using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Use_Transactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions.AmountTransaction",
                schema: "investment-strategies");

            migrationBuilder.DropTable(
                name: "Transactions.VolumeTransaction",
                schema: "investment-strategies");

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "investment-strategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    FinancialAssetId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FinancialAssetId",
                schema: "investment-strategies",
                table: "Transactions",
                column: "FinancialAssetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "investment-strategies");

            migrationBuilder.CreateTable(
                name: "Transactions.AmountTransaction",
                schema: "investment-strategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    CashId = table.Column<Guid>(type: "uuid", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions.AmountTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions.AmountTransaction_FinancialAsset.Cash_CashId",
                        column: x => x.CashId,
                        principalSchema: "investment-strategies",
                        principalTable: "FinancialAsset.Cash",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions.VolumeTransaction",
                schema: "investment-strategies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CoiTreasuryBondId = table.Column<Guid>(type: "uuid", nullable: true),
                    EdoTreasuryBondId = table.Column<Guid>(type: "uuid", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Volume = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions.VolumeTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions.VolumeTransaction_FinancialAsset.CoiTreasuryBo~",
                        column: x => x.CoiTreasuryBondId,
                        principalSchema: "investment-strategies",
                        principalTable: "FinancialAsset.CoiTreasuryBond",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions.VolumeTransaction_FinancialAsset.EdoTreasuryBo~",
                        column: x => x.EdoTreasuryBondId,
                        principalSchema: "investment-strategies",
                        principalTable: "FinancialAsset.EdoTreasuryBond",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions.AmountTransaction_CashId",
                schema: "investment-strategies",
                table: "Transactions.AmountTransaction",
                column: "CashId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions.VolumeTransaction_CoiTreasuryBondId",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction",
                column: "CoiTreasuryBondId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions.VolumeTransaction_EdoTreasuryBondId",
                schema: "investment-strategies",
                table: "Transactions.VolumeTransaction",
                column: "EdoTreasuryBondId");
        }
    }
}
