using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.Users.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class User_Module_Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Role_Value = table.Column<string>(type: "text", nullable: true),
                    Role_GrantedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Role_GrantedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Subscription_Value = table.Column<string>(type: "text", nullable: true),
                    Subscription_GrantedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Subscription_GrantedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Subscription_ExpiredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Subscription_ChangeSource = table.Column<int>(type: "integer", nullable: false),
                    ConfirmationKey = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "users");
        }
    }
}
