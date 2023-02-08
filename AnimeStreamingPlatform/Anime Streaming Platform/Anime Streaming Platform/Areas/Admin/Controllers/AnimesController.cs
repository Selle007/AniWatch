using Anime_Streaming_Platform.Data;
using Anime_Streaming_Platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Anime_Streaming_Platform.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Animes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Animes.ToListAsync());
        }

        // GET: Admin/Animes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animes == null)
            {
                return NotFound();
            }

            var anime = await _context.Animes
                .FirstOrDefaultAsync(m => m.AnimeId == id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // GET: Admin/Animes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Animes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimeId,AnimeName,AnimeDescription,AnimeStudio,Image")] Anime anime, IFormFile image)
        {
            string propName = anime.AnimeName;
            string imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", propName);
            if (!Directory.Exists(imageFolderPath))
            {
                Directory.CreateDirectory(imageFolderPath);
            }
            string fileName = propName + ".png";
            string filePath = Path.Combine(imageFolderPath, fileName);
            string dbFilePath = "../images/" + propName + "/" + fileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            // Save image path to database
            anime.Image = dbFilePath;
            _context.Add(anime);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Admin/Animes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animes == null)
            {
                return NotFound();
            }

            var anime = await _context.Animes.FindAsync(id);
            if (anime == null)
            {
                return NotFound();
            }
            return View(anime);
        }

        // POST: Admin/Animes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimeId,AnimeName,AnimeDescription,AnimeStudio,Image")] Anime anime)
        {
            if (id != anime.AnimeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeExists(anime.AnimeId))
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
            return View(anime);
        }

        // GET: Admin/Animes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animes == null)
            {
                return NotFound();
            }

            var anime = await _context.Animes
                .FirstOrDefaultAsync(m => m.AnimeId == id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // POST: Admin/Animes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Animes'  is null.");
            }
            var anime = await _context.Animes.FindAsync(id);
            if (anime != null)
            {
                _context.Animes.Remove(anime);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeExists(int id)
        {
            return _context.Animes.Any(e => e.AnimeId == id);
        }


        public async Task<IActionResult> UploadAnimeImage(int? id)
        {
            if (id == null || _context.Animes == null)
            {
                return NotFound();
            }

            var anime = await _context.Animes.FindAsync(id);
            if (anime == null)
            {
                return NotFound();
            }
            return View(anime);
        }

        [HttpPost]
        public IActionResult UploadAnimeImage(int id, IFormFile image)
        {
            try
            {
                var anime = _context.Animes.FirstOrDefault(m => m.AnimeId == id);

                string propName = anime.AnimeName;
                // Create user's folder if it doesn't exist

                string imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images", propName);
                if (!Directory.Exists(imageFolderPath))
                {
                    Directory.CreateDirectory(imageFolderPath);
                }



                // Save image to user's folder
                string fileName = propName+".png";
                string filePath = Path.Combine(imageFolderPath, fileName);
                string dbFilePath = "../images/" + propName+"/" + fileName;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                // Save image path to database

                anime.Image = dbFilePath;
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
