using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Configration.Migrations
{
    public partial class UpdateColorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HEX",
                table: "Color",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RGB",
                table: "Color",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HEX",
                table: "Color");

            migrationBuilder.DropColumn(
                name: "RGB",
                table: "Color");
        }
    }
}
