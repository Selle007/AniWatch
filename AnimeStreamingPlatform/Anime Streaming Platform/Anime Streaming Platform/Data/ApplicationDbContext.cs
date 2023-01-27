using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Anime_Streaming_Platform.Models;

namespace Anime_Streaming_Platform.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

        public DbSet<Anime>? Animes { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<AnimeCategories>? AnimeCategories { get; set; }
        public DbSet<Episode>? Episodes { get; set; }
        public DbSet<Manga>? Mangas { get; set; }
        public DbSet<MangaChapter>? MangaChapters { get; set; }

    }
}