using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Related_Portfolios_And_Collaborators_To_Investment_Strategy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Collaborators",
                schema: "investment-strategies",
                table: "InvestmentStrategies");

            migrationBuilder.DropColumn(
                name: "Portfolios",
                schema: "investment-strategies",
                table: "InvestmentStrategies");

            migrationBuilder.CreateTable(
                name: "RelatedCollaborators",
                schema: "investment-strategies",
                columns: table => new
                {
                    InvestmentStrategyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedCollaborators", x => new { x.InvestmentStrategyId, x.Id });
                    table.ForeignKey(
                        name: "FK_RelatedCollaborators_InvestmentStrategies_InvestmentStrateg~",
                        column: x => x.InvestmentStrategyId,
                        principalSchema: "investment-strategies",
                        principalTable: "InvestmentStrategies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelatedPortfolios",
                schema: "investment-strategies",
                columns: table => new
                {
                    InvestmentStrategyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedPortfolios", x => new { x.InvestmentStrategyId, x.Id });
                    table.ForeignKey(
                        name: "FK_RelatedPortfolios_InvestmentStrategies_InvestmentStrategyId",
                        column: x => x.InvestmentStrategyId,
                        principalSchema: "investment-strategies",
                        principalTable: "InvestmentStrategies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedCollaborators",
                schema: "investment-strategies");

            migrationBuilder.DropTable(
                name: "RelatedPortfolios",
                schema: "investment-strategies");

            migrationBuilder.AddColumn<string>(
                name: "Collaborators",
                schema: "investment-strategies",
                table: "InvestmentStrategies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Portfolios",
                schema: "investment-strategies",
                table: "InvestmentStrategies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
