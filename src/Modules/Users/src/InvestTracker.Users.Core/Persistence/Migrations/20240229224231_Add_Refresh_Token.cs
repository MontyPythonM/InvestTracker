using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.Users.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Refresh_Token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshToken_ExpiredAt",
                schema: "users",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken_Token",
                schema: "users",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken_ExpiredAt",
                schema: "users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshToken_Token",
                schema: "users",
                table: "Users");
        }
    }
}
