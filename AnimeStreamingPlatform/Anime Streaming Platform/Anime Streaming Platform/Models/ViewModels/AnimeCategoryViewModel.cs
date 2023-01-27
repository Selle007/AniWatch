using Anime_Streaming_Platform.Models;

namespace Anime_Streaming_Platform.ViewModels
{
    public class AnimeCategoryViewModel
    {
        public List<Anime> Animes { get; set; }
        public List<Category> Categories { get; set; }
        public int? AnimeId { get; set; }
        public int? CategoryId { get; set; }
        public AnimeCategories AnimeCategories { get; set; }
    }
}
