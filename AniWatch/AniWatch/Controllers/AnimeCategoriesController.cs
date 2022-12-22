using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AniWatch.Data;
using AniWatch.Models;
using AniWatch.ViewModels;

namespace AniWatch.Controllers
{
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
        /*
        // GET: AnimeCategories/Create
        public IActionResult Create()
        {
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: AnimeCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimeId,CategoryId")] AnimeCategories animeCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animeCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId", animeCategories.AnimeId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", animeCategories.CategoryId);
            return View(animeCategories);
        }
        */


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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnimeCategories == null)
            {
                return NotFound();
            }

            var animeCategories = await _context.AnimeCategories.FindAsync(id);
            if (animeCategories == null)
            {
                return NotFound();
            }
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId", animeCategories.AnimeId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", animeCategories.CategoryId);
            return View(animeCategories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimeId,CategoryId")] AnimeCategories animeCategories)
        {
            if (id != animeCategories.AnimeCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animeCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeCategoriesExists(animeCategories.AnimeCategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId", animeCategories.AnimeId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", animeCategories.CategoryId);
            return View(animeCategories);
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
