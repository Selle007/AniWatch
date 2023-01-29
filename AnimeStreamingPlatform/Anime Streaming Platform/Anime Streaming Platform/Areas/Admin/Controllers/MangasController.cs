using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anime_Streaming_Platform.Data;
using Anime_Streaming_Platform.Models;
using Anime_Streaming_Platform.ViewModels;

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
            List<Anime> animes = this._context.Animes.ToList(); ;
            MangaAnimeViewModel model = new MangaAnimeViewModel();
            model.Animes = animes;
            return View(model);
        }

        // POST: Mangas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      /*  [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MangaId,MangaName,MangaDesc,AnimeName")] Manga manga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeName", manga.AnimeId);
            return View(manga);
        }
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MangaAnimeViewModel model, [Bind("MangaId,MangaName,MangaDesc,AnimeName")] Manga manga)
        {
            var anime = this._context.Animes.Find(model.AnimeId);


            manga.Anime = anime;



            this._context.Mangas.Add(manga);
            this._context.SaveChanges();

            return RedirectToAction(nameof(Index));
            return View();
        }


        // GET: Mangas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var manga = _context.Mangas.Find(id);
            var anime = _context.Animes.ToList();

            var model = new MangaAnimeViewModel { Animes = anime, Mangas = manga };
            return View(model);
        }

        // POST: Mangas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MangaId,MangaName,MangaDesc,AnimeId")] Manga manga, MangaAnimeViewModel model)
        {
             manga = _context.Mangas.Find(id);
            manga.AnimeId = model.Mangas.AnimeId;
            manga.MangaName = model.Mangas.MangaName;
            manga.MangaDesc = model.Mangas.MangaDesc;
  

            _context.SaveChanges();
            return RedirectToAction("Index");

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
