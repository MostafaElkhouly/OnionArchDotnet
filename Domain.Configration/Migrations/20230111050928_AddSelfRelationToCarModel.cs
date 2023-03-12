using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Configration.Migrations
{
    public partial class AddSelfRelationToCarModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarModelId",
                table: "CarModel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarModel_CarModelId",
                table: "CarModel",
                column: "CarModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModel_CarModel_CarModelId",
                table: "CarModel",
                column: "CarModelId",
                principalTable: "CarModel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModel_CarModel_CarModelId",
                table: "CarModel");

            migrationBuilder.DropIndex(
                name: "IX_CarModel_CarModelId",
                table: "CarModel");

            migrationBuilder.DropColumn(
                name: "CarModelId",
                table: "CarModel");
        }
    }
}
