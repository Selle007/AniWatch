using System.ComponentModel.DataAnnotations;

namespace AniWatch.Models
{
    public class Anime
    {
        [Key]
        public int AnimeId { get; set; }
        public string? AnimeName { get; set; }
        public string? AnimeDescription { get; set; }
        public string? AnimeStudio { get; set; }
    }
}
