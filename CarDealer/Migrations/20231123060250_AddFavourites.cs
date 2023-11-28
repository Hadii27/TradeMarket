using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDealer.Migrations
{
    public partial class AddFavourites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "favourite",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favourite", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "favouriteDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavouriteID = table.Column<int>(type: "int", nullable: false),
                    sellId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favouriteDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_favouriteDetails_favourite_FavouriteID",
                        column: x => x.FavouriteID,
                        principalTable: "favourite",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_favouriteDetails_sells_sellId",
                        column: x => x.sellId,
                        principalTable: "sells",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_favouriteDetails_FavouriteID",
                table: "favouriteDetails",
                column: "FavouriteID");

            migrationBuilder.CreateIndex(
                name: "IX_favouriteDetails_sellId",
                table: "favouriteDetails",
                column: "sellId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favouriteDetails");

            migrationBuilder.DropTable(
                name: "favourite");
        }
    }
}
