using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniWatch.Data.Migrations
{
    public partial class Create_MangaChapter_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MangaChapters",
                columns: table => new
                {
                    MangaChapterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaChapterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MangaChapterNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: false),
                    MangaChapterURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaChapters", x => x.MangaChapterId);
                    table.ForeignKey(
                        name: "FK_MangaChapters_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "MangaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MangaChapters_MangaId",
                table: "MangaChapters",
                column: "MangaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MangaChapters");
        }
    }
}
