
using System.ComponentModel.DataAnnotations;

namespace Anime_Streaming_Platform.Models
{

    public class AnimeCategories
    {
        [Key]
        public int AnimeCategoryId { get; set; }
        public Anime Anime { get; set; }
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public int? AnimeId { get; set; }


    }
}
