using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anime_Streaming_Platform.Data;
using Anime_Streaming_Platform.Models;

namespace Anime_Streaming_Platform.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MangasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MangasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mangas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Mangas.Include(m => m.Anime);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Mangas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mangas == null)
            {
                return NotFound();
            }

            var manga = await _context.Mangas
                .Include(m => m.Anime)
                .FirstOrDefaultAsync(m => m.MangaId == id);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }

        // GET: Mangas/Create
        public IActionResult Create()
        {
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId");
            return View();
        }

        // POST: Mangas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MangaId,MangaName,MangaDesc,AnimeId")] Manga manga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId", manga.AnimeId);
            return View(manga);
        }

        // GET: Mangas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mangas == null)
            {
                return NotFound();
            }

            var manga = await _context.Mangas.FindAsync(id);
            if (manga == null)
            {
                return NotFound();
            }
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId", manga.AnimeId);
            return View(manga);
        }

        // POST: Mangas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MangaId,MangaName,MangaDesc,AnimeId")] Manga manga)
        {
            if (id != manga.MangaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MangaExists(manga.MangaId))
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
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId", manga.AnimeId);
            return View(manga);
        }

        // GET: Mangas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mangas == null)
            {
                return NotFound();
            }

            var manga = await _context.Mangas
                .Include(m => m.Anime)
                .FirstOrDefaultAsync(m => m.MangaId == id);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }

        // POST: Mangas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mangas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Mangas'  is null.");
            }
            var manga = await _context.Mangas.FindAsync(id);
            if (manga != null)
            {
                _context.Mangas.Remove(manga);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MangaExists(int id)
        {
          return _context.Mangas.Any(e => e.MangaId == id);
        }

        public async Task<IActionResult> UploadMangaImage(int? id)
        {
            if (id == null || _context.Mangas == null)
            {
                return NotFound();
            }

            var manga = await _context.Mangas.FindAsync(id);
            if (manga == null)
            {
                return NotFound();
            }
            return View(manga);
        }

        [HttpPost]
        public IActionResult UploadMangaImage(int id, IFormFile image)
        {
            try
            {
                var manga = _context.Mangas.FirstOrDefault(m => m.MangaId == id);

                string propName = manga.MangaName;
                // Create user's folder if it doesn't exist
                string imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "frontend", "src", "images", propName);
                //string imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images", propName);
                if (!Directory.Exists(imageFolderPath))
                {
                    Directory.CreateDirectory(imageFolderPath);
                }
                // string sourcePath = Path.Combine(Directory.GetCurrentDirectory(), "New Proj", "Anime Streaming Platform", "Anime Streaming Platform","wwwroot", "images");
                //  string destinationPath = Path.Combine(Directory.GetCurrentDirectory(), "frontend", "src", "images");


                // Save image to user's folder
                string fileName = propName + ".png";
                string filePath = Path.Combine(imageFolderPath, fileName);
                string dbFilePath = "../images/" + propName + "/" + fileName;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                // Save image path to database

                manga.Image = dbFilePath;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while uploading the image. " + ex.Message);
            }
        }
    }
}
