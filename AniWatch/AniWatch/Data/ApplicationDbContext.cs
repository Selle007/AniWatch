using AniWatch.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AniWatch.Data
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
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Anime>? Animes { get; set; }
        public DbSet<AnimeCategories> AnimeCategories { get; set; }
        public DbSet<Manga> Mangas { get; set; }



    }
}