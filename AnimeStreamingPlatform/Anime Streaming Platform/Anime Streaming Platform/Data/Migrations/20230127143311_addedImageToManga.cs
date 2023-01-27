using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Streaming_Platform.Data.Migrations
{
    public partial class addedImageToManga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Mangas");
        }
    }
}
