using System.ComponentModel.DataAnnotations;

namespace AniWatch.Models
{
    public class MangaChapter
    {
        [Key]
        public int MangaChapterId { get; set; }
        public string? MangaChapterName { get; set; }
        public string? MangaChapterNumber { get; set; }
        public Manga? Manga { get; set; }
        public int MangaId { get; set; }
        public string? MangaChapterURL { get; set; }
    }
}
