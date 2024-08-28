using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class monye : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeeklyHolidayCount",
                table: "GeneralSettings");

            migrationBuilder.AddColumn<decimal>(
                name: "bonusPerHoure",
                table: "GeneralSettings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "rivalPerHour",
                table: "GeneralSettings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bonusPerHoure",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "rivalPerHour",
                table: "GeneralSettings");

            migrationBuilder.AddColumn<int>(
                name: "WeeklyHolidayCount",
                table: "GeneralSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
