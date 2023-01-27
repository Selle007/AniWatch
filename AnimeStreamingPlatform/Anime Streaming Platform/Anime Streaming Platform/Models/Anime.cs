using System.ComponentModel.DataAnnotations;

namespace Anime_Streaming_Platform.Models
{
    public class Anime
    {
        [Key]
        public int AnimeId { get; set; }
        public string? AnimeName { get; set; }
        public string? AnimeDescription { get; set; }
        public string? AnimeStudio { get; set; }
        public string Image { get; set; }
    }
}
