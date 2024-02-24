using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.Users.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Reset_Password_In_User_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPassword_ExpiredAt",
                schema: "users",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPassword_InvokeAt",
                schema: "users",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResetPassword_Key",
                schema: "users",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPassword_ExpiredAt",
                schema: "users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResetPassword_InvokeAt",
                schema: "users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResetPassword_Key",
                schema: "users",
                table: "Users");
        }
    }
}
