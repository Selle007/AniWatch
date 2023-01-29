using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Anime_Streaming_Platform.Models
{
    public class Bookmark
    {
        [Key]
        public int BookmarkId { get; set; }
        public string UserId { get; set; }
        public  User User { get; set; }
        public int AnimeId { get; set; }
        public  Anime Anime { get; set; }
    }

}
