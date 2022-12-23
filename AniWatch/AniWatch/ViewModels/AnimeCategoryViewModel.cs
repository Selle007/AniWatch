using AniWatch.Models;

namespace AniWatch.ViewModels
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
