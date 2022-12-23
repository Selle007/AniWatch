using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniWatch.Data.Migrations
{
    public partial class Create_Manga_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    MangaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MangaDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.MangaId);
                    table.ForeignKey(
                        name: "FK_Mangas_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "AnimeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_AnimeId",
                table: "Mangas",
                column: "AnimeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mangas");
        }
    }
}
