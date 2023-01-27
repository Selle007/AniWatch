using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anime_Streaming_Platform.Data;
using Anime_Streaming_Platform.Models;
using Anime_Streaming_Platform.ViewModels;

namespace Anime_Streaming_Platform.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnimeCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimeCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AnimeCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AnimeCategories.Include(a => a.Anime).Include(a => a.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AnimeCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnimeCategories == null)
            {
                return NotFound();
            }

            var animeCategories = await _context.AnimeCategories
                .Include(a => a.Anime)
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.AnimeCategoryId == id);
            if (animeCategories == null)
            {
                return NotFound();
            }

            return View(animeCategories);
        }
        
        public IActionResult Create()
        {
            List<Anime> animes = this._context.Animes.ToList();
            List<Category> categories = this._context.Categories.ToList();
            AnimeCategoryViewModel model = new AnimeCategoryViewModel();
            model.Animes = animes;
            model.Categories = categories;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimeCategoryViewModel model)
        {
            var anime = this._context.Animes.Find(model.AnimeId);
            var category = this._context.Categories.Find(model.CategoryId);

            var animeCateogry = new AnimeCategories();
            animeCateogry.Anime = anime;
            animeCateogry.Category = category;


            this._context.AnimeCategories.Add(animeCateogry);
            this._context.SaveChanges();

            return RedirectToAction(nameof(Index));
            return View();
        }
        // GET: AnimeCategories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var animeCategory = _context.AnimeCategories.Find(id);
            var anime = _context.Animes.ToList();
            var category = _context.Categories.ToList();
            var model = new AnimeCategoryViewModel { Animes = anime, Categories = category, AnimeCategories = animeCategory };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AnimeCategoryViewModel model)
        {
            
                var animeCategory = _context.AnimeCategories.Find(id);
                animeCategory.AnimeId = model.AnimeCategories.AnimeId;
                animeCategory.CategoryId = model.AnimeCategories.CategoryId;

                _context.SaveChanges();
                return RedirectToAction("Index");
            
        }

        // GET: AnimeCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnimeCategories == null)
            {
                return NotFound();
            }

            var animeCategories = await _context.AnimeCategories
                .Include(a => a.Anime)
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.AnimeCategoryId == id);
            if (animeCategories == null)
            {
                return NotFound();
            }

            return View(animeCategories);
        }

        // POST: AnimeCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnimeCategories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AnimeCategories'  is null.");
            }
            var animeCategories = await _context.AnimeCategories.FindAsync(id);
            if (animeCategories != null)
            {
                _context.AnimeCategories.Remove(animeCategories);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeCategoriesExists(int id)
        {
          return _context.AnimeCategories.Any(e => e.AnimeCategoryId == id);
        }
    }
}
