using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManangement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TradingName",
                table: "Driver");

            migrationBuilder.RenameColumn(
                name: "ComplexName",
                table: "Vehicle",
                newName: "Year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Vehicle",
                newName: "ComplexName");

            migrationBuilder.AddColumn<string>(
                name: "TradingName",
                table: "Driver",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
