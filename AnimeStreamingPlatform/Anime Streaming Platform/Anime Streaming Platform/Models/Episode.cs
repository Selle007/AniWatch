using System.ComponentModel.DataAnnotations;

namespace Anime_Streaming_Platform.Models
{
    public class Episode
    {
        [Key]
        public int EpisodeId { get; set; }
        public int EpisodeNumber { get; set; }  
        public string EpisodeTitle { get; set; }
        public string EpisodeUrl { get; set; }
        public Anime? Anime { get; set; }
        public int? AnimeId { get; set; }
    }
}
