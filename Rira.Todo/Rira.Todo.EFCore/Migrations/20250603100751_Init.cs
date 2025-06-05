using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rira.Todo.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AppSettings",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Theme = table.Column<string>(type: "varchar(255)", nullable: false),
                    Language = table.Column<string>(type: "varchar(255)", nullable: false),
                    Version = table.Column<string>(type: "varchar(255)", nullable: false),
                    NotificationsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    Username = table.Column<string>(type: "varchar(255)", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    TwoFactorAuthenticationEnabled = table.Column<bool>(type: "bit", nullable: false),
                    BatterySaverMode = table.Column<bool>(type: "bit", nullable: false),
                    StorageLocation = table.Column<string>(type: "varchar(255)", nullable: false),
                    CurrentPlan = table.Column<string>(type: "varchar(255)", nullable: false),
                    PlanRenewalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HelpCenterURL = table.Column<string>(type: "varchar(255)", nullable: false),
                    FeedbackEmail = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoItem",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItem", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AppSettings",
                columns: new[] { "Id", "BatterySaverMode", "CurrentPlan", "Email", "FeedbackEmail", "HelpCenterURL", "Language", "Name", "NotificationsEnabled", "PlanRenewalDate", "StorageLocation", "Theme", "TwoFactorAuthenticationEnabled", "Username", "Version" },
                values: new object[] { new Guid("3f9a8dbf-78c9-4ef6-bdbc-98f7802db983"), false, "Free", "admin@app.com", "feedback@app.com", "https://help.app.com", "English", "DefaultSettings", true, new DateTime(2025, 4, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Local", "Light", false, "admin", "1.0.0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TodoItem",
                schema: "dbo");
        }
    }
}
