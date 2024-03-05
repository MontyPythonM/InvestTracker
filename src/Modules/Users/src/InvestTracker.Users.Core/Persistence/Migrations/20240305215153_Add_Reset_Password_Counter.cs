using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.Users.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Reset_Password_Counter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResetPassword_Counter",
                schema: "users",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.Sql(@"UPDATE users.""Users"" SET ""ResetPassword_Counter"" = 1 WHERE ""ResetPassword_Key"" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPassword_Counter",
                schema: "users",
                table: "Users");
        }
    }
}
