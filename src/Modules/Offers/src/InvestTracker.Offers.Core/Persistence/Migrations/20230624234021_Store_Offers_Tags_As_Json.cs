using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.Offers.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Store_Offers_Tags_As_Json : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferTags",
                schema: "offers");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "offers",
                table: "Offers",
                type: "numeric(2)",
                precision: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(2,0)",
                oldPrecision: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                schema: "offers",
                table: "Offers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                schema: "offers",
                table: "Offers");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "offers",
                table: "Offers",
                type: "numeric(2,0)",
                precision: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(2)",
                oldPrecision: 2,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OfferTags",
                schema: "offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OfferId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferTags_Offers_OfferId",
                        column: x => x.OfferId,
                        principalSchema: "offers",
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferTags_OfferId",
                schema: "offers",
                table: "OfferTags",
                column: "OfferId");
        }
    }
}
