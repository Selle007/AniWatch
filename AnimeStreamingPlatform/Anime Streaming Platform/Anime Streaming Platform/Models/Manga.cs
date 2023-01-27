using System.ComponentModel.DataAnnotations;

namespace Anime_Streaming_Platform.Models
{
    public class Manga
    {
        [Key]
        public int MangaId { get; set; }
        public string? MangaName { get; set; }
        public string? MangaDesc { get; set; }
        public Anime? Anime { get; set; }
        public int? AnimeId { get; set; }
        public string? Image { get; set; }
    }
}
