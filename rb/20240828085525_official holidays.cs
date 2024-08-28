using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class officialholidays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GeneralSettings_EmployeeId",
                table: "GeneralSettings",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralSettings_Employee_EmployeeId",
                table: "GeneralSettings",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralSettings_Employee_EmployeeId",
                table: "GeneralSettings");

            migrationBuilder.DropIndex(
                name: "IX_GeneralSettings_EmployeeId",
                table: "GeneralSettings");
        }
    }
}
