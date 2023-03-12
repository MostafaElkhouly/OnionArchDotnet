using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Configration.Migrations
{
    public partial class CSMS_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobCard_AspNetUsers_MechanicId",
                table: "JobCard");

            migrationBuilder.DropIndex(
                name: "IX_JobCard_MechanicId",
                table: "JobCard");

            migrationBuilder.DropColumn(
                name: "MechanicId",
                table: "JobCard");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MechanicId",
                table: "JobCard",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobCard_MechanicId",
                table: "JobCard",
                column: "MechanicId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobCard_AspNetUsers_MechanicId",
                table: "JobCard",
                column: "MechanicId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
