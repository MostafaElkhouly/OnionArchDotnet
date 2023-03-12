using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Configration.Migrations
{
    public partial class CSMS_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SparPart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SparName",
                table: "SparPart",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SparPart");

            migrationBuilder.DropColumn(
                name: "SparName",
                table: "SparPart");
        }
    }
}
