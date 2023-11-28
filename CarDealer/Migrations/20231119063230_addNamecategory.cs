using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDealer.Migrations
{
    public partial class addNamecategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "subCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "subCategories");
        }
    }
}
