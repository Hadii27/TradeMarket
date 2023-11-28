using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDealer.Migrations
{
    public partial class addNationalityID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "AspNetUsers",
                newName: "Nationalality");

            migrationBuilder.AddColumn<int>(
                name: "NationalityId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Nationalality",
                table: "AspNetUsers",
                newName: "Nationality");
        }
    }
}
