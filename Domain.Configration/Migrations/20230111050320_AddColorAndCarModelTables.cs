using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Configration.Migrations
{
    public partial class AddColorAndCarModelTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarModel",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Car");

            migrationBuilder.AddColumn<Guid>(
                name: "CarModelId",
                table: "Car",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ColorId",
                table: "Car",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CarModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateOfCreate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateOfCreate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarModelId",
                table: "Car",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_ColorId",
                table: "Car",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarModel_CarModelId",
                table: "Car",
                column: "CarModelId",
                principalTable: "CarModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Color_ColorId",
                table: "Car",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarModel_CarModelId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_Color_ColorId",
                table: "Car");

            migrationBuilder.DropTable(
                name: "CarModel");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropIndex(
                name: "IX_Car_CarModelId",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_ColorId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "CarModelId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Car");

            migrationBuilder.AddColumn<string>(
                name: "CarModel",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
