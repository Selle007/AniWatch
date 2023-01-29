using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Anime_Streaming_Platform.Models;

namespace Anime_Streaming_Platform.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Bookmark>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bookmarks)
            .HasForeignKey(b => b.UserId);

            builder.Entity<Bookmark>()
            .HasOne(b => b.Anime)
            .WithMany(a => a.Bookmarks)
            .HasForeignKey(b => b.AnimeId);

            builder.Entity<User>().ToTable("Users");


        }

        public DbSet<Anime>? Animes { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<AnimeCategories>? AnimeCategories { get; set; }
        public DbSet<Episode>? Episodes { get; set; }
        public DbSet<Manga>? Mangas { get; set; }
        public DbSet<MangaChapter>? MangaChapters { get; set; }
        public DbSet<Bookmark>? Bookmarks { get; set; }

    }
}