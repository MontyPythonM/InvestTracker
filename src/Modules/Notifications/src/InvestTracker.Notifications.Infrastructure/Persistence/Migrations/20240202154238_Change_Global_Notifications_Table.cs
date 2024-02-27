using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestTracker.Notifications.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Change_Global_Notifications_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalNotificationSetup",
                schema: "notifications");

            migrationBuilder.RenameColumn(
                name: "NotificationSetup_PortfoliosActivity",
                schema: "notifications",
                table: "Receivers",
                newName: "PersonalSettings_PortfoliosActivity");

            migrationBuilder.RenameColumn(
                name: "NotificationSetup_NewCollaborationsActivity",
                schema: "notifications",
                table: "Receivers",
                newName: "PersonalSettings_NewCollaborationsActivity");

            migrationBuilder.RenameColumn(
                name: "NotificationSetup_ModifiedAt",
                schema: "notifications",
                table: "Receivers",
                newName: "PersonalSettings_ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "NotificationSetup_InvestmentStrategiesActivity",
                schema: "notifications",
                table: "Receivers",
                newName: "PersonalSettings_InvestmentStrategiesActivity");

            migrationBuilder.RenameColumn(
                name: "NotificationSetup_Id",
                schema: "notifications",
                table: "Receivers",
                newName: "PersonalSettings_Id");

            migrationBuilder.RenameColumn(
                name: "NotificationSetup_ExistingCollaborationsActivity",
                schema: "notifications",
                table: "Receivers",
                newName: "PersonalSettings_ExistingCollaborationsActivity");

            migrationBuilder.RenameColumn(
                name: "NotificationSetup_EnableNotifications",
                schema: "notifications",
                table: "Receivers",
                newName: "PersonalSettings_EnableNotifications");

            migrationBuilder.RenameColumn(
                name: "NotificationSetup_EnableEmails",
                schema: "notifications",
                table: "Receivers",
                newName: "PersonalSettings_EnableEmails");

            migrationBuilder.RenameColumn(
                name: "NotificationSetup_CreatedAt",
                schema: "notifications",
                table: "Receivers",
                newName: "PersonalSettings_CreatedAt");

            migrationBuilder.RenameColumn(
                name: "NotificationSetup_AssetActivity",
                schema: "notifications",
                table: "Receivers",
                newName: "PersonalSettings_AssetActivity");

            migrationBuilder.RenameColumn(
                name: "NotificationSetup_AdministratorsActivity",
                schema: "notifications",
                table: "Receivers",
                newName: "PersonalSettings_AdministratorsActivity");

            migrationBuilder.CreateTable(
                name: "GlobalSettings",
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
                    table.PrimaryKey("PK_GlobalSettings", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "notifications",
                table: "GlobalSettings",
                columns: new[] { "Id", "AdministratorsActivity", "AssetActivity", "EffectiveFrom", "EnableEmails", "EnableNotifications", "ExistingCollaborationsActivity", "InvestmentStrategiesActivity", "NewCollaborationsActivity", "PortfoliosActivity" },
                values: new object[] { new Guid("b00faf32-301f-45da-8109-f389463b1a42"), false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, true, true, true, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalSettings",
                schema: "notifications");

            migrationBuilder.RenameColumn(
                name: "PersonalSettings_PortfoliosActivity",
                schema: "notifications",
                table: "Receivers",
                newName: "NotificationSetup_PortfoliosActivity");

            migrationBuilder.RenameColumn(
                name: "PersonalSettings_NewCollaborationsActivity",
                schema: "notifications",
                table: "Receivers",
                newName: "NotificationSetup_NewCollaborationsActivity");

            migrationBuilder.RenameColumn(
                name: "PersonalSettings_ModifiedAt",
                schema: "notifications",
                table: "Receivers",
                newName: "NotificationSetup_ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "PersonalSettings_InvestmentStrategiesActivity",
                schema: "notifications",
                table: "Receivers",
                newName: "NotificationSetup_InvestmentStrategiesActivity");

            migrationBuilder.RenameColumn(
                name: "PersonalSettings_Id",
                schema: "notifications",
                table: "Receivers",
                newName: "NotificationSetup_Id");

            migrationBuilder.RenameColumn(
                name: "PersonalSettings_ExistingCollaborationsActivity",
                schema: "notifications",
                table: "Receivers",
                newName: "NotificationSetup_ExistingCollaborationsActivity");

            migrationBuilder.RenameColumn(
                name: "PersonalSettings_EnableNotifications",
                schema: "notifications",
                table: "Receivers",
                newName: "NotificationSetup_EnableNotifications");

            migrationBuilder.RenameColumn(
                name: "PersonalSettings_EnableEmails",
                schema: "notifications",
                table: "Receivers",
                newName: "NotificationSetup_EnableEmails");

            migrationBuilder.RenameColumn(
                name: "PersonalSettings_CreatedAt",
                schema: "notifications",
                table: "Receivers",
                newName: "NotificationSetup_CreatedAt");

            migrationBuilder.RenameColumn(
                name: "PersonalSettings_AssetActivity",
                schema: "notifications",
                table: "Receivers",
                newName: "NotificationSetup_AssetActivity");

            migrationBuilder.RenameColumn(
                name: "PersonalSettings_AdministratorsActivity",
                schema: "notifications",
                table: "Receivers",
                newName: "NotificationSetup_AdministratorsActivity");

            migrationBuilder.CreateTable(
                name: "GlobalNotificationSetup",
                schema: "notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdministratorsActivity = table.Column<bool>(type: "boolean", nullable: false),
                    AssetActivity = table.Column<bool>(type: "boolean", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EnableEmails = table.Column<bool>(type: "boolean", nullable: false),
                    EnableNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    ExistingCollaborationsActivity = table.Column<bool>(type: "boolean", nullable: false),
                    InvestmentStrategiesActivity = table.Column<bool>(type: "boolean", nullable: false),
                    NewCollaborationsActivity = table.Column<bool>(type: "boolean", nullable: false),
                    PortfoliosActivity = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalNotificationSetup", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "notifications",
                table: "GlobalNotificationSetup",
                columns: new[] { "Id", "AdministratorsActivity", "AssetActivity", "EffectiveFrom", "EnableEmails", "EnableNotifications", "ExistingCollaborationsActivity", "InvestmentStrategiesActivity", "NewCollaborationsActivity", "PortfoliosActivity" },
                values: new object[] { new Guid("f195c206-a475-4bdb-8a70-2575f0ab35b6"), false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, true, true, true, true });
        }
    }
}
