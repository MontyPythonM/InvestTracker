using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.Offers.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Change_Avatar_To_Byte_Array_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                schema: "offers",
                table: "Advisors");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                schema: "offers",
                table: "Advisors",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                schema: "offers",
                table: "Advisors");

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                schema: "offers",
                table: "Advisors",
                type: "text",
                nullable: true);
        }
    }
}
