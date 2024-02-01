using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.Notifications.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "notifications");

            migrationBuilder.CreateTable(
                name: "GlobalNotificationSetup",
                schema: "notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EnableNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    EnableEmails = table.Column<bool>(type: "boolean", nullable: false),
                    AdministratorsActivity = table.Column<bool>(type: "boolean", nullable: false),
                    InvestmentStrategiesActivity = table.Column<bool>(type: "boolean", nullable: false),
                    PortfoliosActivity = table.Column<bool>(type: "boolean", nullable: false),
                    AssetActivity = table.Column<bool>(type: "boolean", nullable: false),
                    ExistingCollaborationsActivity = table.Column<bool>(type: "boolean", nullable: false),
                    NewCollaborationsActivity = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalNotificationSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receivers",
                schema: "notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Subscription = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    NotificationSetup_CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NotificationSetup_ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NotificationSetup_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NotificationSetup_EnableNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    NotificationSetup_EnableEmails = table.Column<bool>(type: "boolean", nullable: false),
                    NotificationSetup_AdministratorsActivity = table.Column<bool>(type: "boolean", nullable: false),
                    NotificationSetup_InvestmentStrategiesActivity = table.Column<bool>(type: "boolean", nullable: false),
                    NotificationSetup_PortfoliosActivity = table.Column<bool>(type: "boolean", nullable: false),
                    NotificationSetup_AssetActivity = table.Column<bool>(type: "boolean", nullable: false),
                    NotificationSetup_ExistingCollaborationsActivity = table.Column<bool>(type: "boolean", nullable: false),
                    NotificationSetup_NewCollaborationsActivity = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivers", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "notifications",
                table: "GlobalNotificationSetup",
                columns: new[] { "Id", "AdministratorsActivity", "AssetActivity", "EffectiveFrom", "EnableEmails", "EnableNotifications", "ExistingCollaborationsActivity", "InvestmentStrategiesActivity", "NewCollaborationsActivity", "PortfoliosActivity" },
                values: new object[] { new Guid("f195c206-a475-4bdb-8a70-2575f0ab35b6"), false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, true, true, true, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalNotificationSetup",
                schema: "notifications");

            migrationBuilder.DropTable(
                name: "Receivers",
                schema: "notifications");
        }
    }
}
