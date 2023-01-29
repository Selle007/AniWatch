using Anime_Streaming_Platform.Models;


namespace Anime_Streaming_Platform.ViewModels
{
    public class MangaAnimeViewModel
    {
        public List<Anime> Animes { get; set; }
        public int? AnimeId { get; set; }
        public int MangaId { get; set; }
        public string? MangaName { get; set; }
        public string? MangaDesc { get; set; }
        public string? Image { get; set; }
        public Manga Mangas { get; set; }
    }
}
