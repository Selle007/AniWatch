using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Streaming_Platform.Data.Migrations
{
    public partial class addedAnimeCategoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeCategories",
                columns: table => new
                {
                    AnimeCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    AnimeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeCategories", x => x.AnimeCategoryId);
                    table.ForeignKey(
                        name: "FK_AnimeCategories_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "AnimeId");
                    table.ForeignKey(
                        name: "FK_AnimeCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeCategories_AnimeId",
                table: "AnimeCategories",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeCategories_CategoryId",
                table: "AnimeCategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeCategories");
        }
    }
}
