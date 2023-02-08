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
    public class EpisodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EpisodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Episodes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Episodes.Include(e => e.Anime);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Episodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Episodes == null)
            {
                return NotFound();
            }

            var episode = await _context.Episodes
                .Include(e => e.Anime)
                .FirstOrDefaultAsync(m => m.EpisodeId == id);
            if (episode == null)
            {
                return NotFound();
            }

            return View(episode);
        }

        // GET: Episodes/Create
        public IActionResult Create()
        {
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId");
            return View();
        }

        // POST: Episodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EpisodeId,EpisodeNumber,EpisodeTitle,EpisodeUrl,AnimeId")] Episode episode, IFormFile video)
        {

            var anime = _context.Animes.FirstOrDefault(m => m.AnimeId == episode.AnimeId);

            string propName = anime.AnimeName;
            string videoFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", propName);
            if (!Directory.Exists(videoFolderPath))
            {
                Directory.CreateDirectory(videoFolderPath);
            }

            string fileName = "Episode" + episode.EpisodeNumber + ".mp4";
            string filePath = Path.Combine(videoFolderPath, fileName);
            string dbFilePath = "../../images/" + propName + "/" + fileName;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                video.CopyTo(stream);
            }

            // Save image path to database

            episode.EpisodeUrl = dbFilePath;

            _context.Add(episode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId", episode.AnimeId);
        }

        // GET: Episodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Episodes == null)
            {
                return NotFound();
            }

            var episode = await _context.Episodes.FindAsync(id);
            if (episode == null)
            {
                return NotFound();
            }
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId", episode.AnimeId);
            return View(episode);
        }

        // POST: Episodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EpisodeId,EpisodeNumber,EpisodeTitle,EpisodeUrl,AnimeId")] Episode episode)
        {
            if (id != episode.EpisodeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(episode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodeExists(episode.EpisodeId))
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
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId", episode.AnimeId);
            return View(episode);
        }

        // GET: Episodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Episodes == null)
            {
                return NotFound();
            }

            var episode = await _context.Episodes
                .Include(e => e.Anime)
                .FirstOrDefaultAsync(m => m.EpisodeId == id);
            if (episode == null)
            {
                return NotFound();
            }

            return View(episode);
        }

        // POST: Episodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Episodes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Episodes'  is null.");
            }
            var episode = await _context.Episodes.FindAsync(id);
            if (episode != null)
            {
                _context.Episodes.Remove(episode);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodeExists(int id)
        {
            return _context.Episodes.Any(e => e.EpisodeId == id);
        }
    }
}
