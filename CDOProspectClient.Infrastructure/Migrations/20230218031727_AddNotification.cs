using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CDOProspectClient.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationEntityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Entity = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NotificationMessage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationEntityTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NotificationObject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    NotificationEntityTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationObject_NotificationEntityTypes_NotificationEntit~",
                        column: x => x.NotificationEntityTypeId,
                        principalTable: "NotificationEntityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActorId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NotifierId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NotificationObjectId = table.Column<int>(type: "int", nullable: false),
                    DateNotified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationObject_NotificationObjectId",
                        column: x => x.NotificationObjectId,
                        principalTable: "NotificationObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "NotificationEntityTypes",
                columns: new[] { "Id", "Entity", "NotificationMessage" },
                values: new object[,]
                {
                    { 1, "Requirement", "Forwarded a requirement" },
                    { 2, "Requirement", "Cancelled the forwarded requirement" },
                    { 3, "Requirement", "Approved the requirement you have submit" },
                    { 4, "Requirement", "Your requirement has been rejected" },
                    { 5, "Appointment", "Someone has set you up with an appointment" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationObject_NotificationEntityTypeId",
                table: "NotificationObject",
                column: "NotificationEntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationObjectId",
                table: "Notifications",
                column: "NotificationObjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationObject");

            migrationBuilder.DropTable(
                name: "NotificationEntityTypes");
        }
    }
}
