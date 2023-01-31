using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CDOProspectClient.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSomeFieldToAgentAndPropertyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Properties",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Agents",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Agents");
        }
    }
}
