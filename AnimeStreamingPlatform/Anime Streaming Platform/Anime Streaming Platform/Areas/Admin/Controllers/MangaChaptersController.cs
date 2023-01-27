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
    public class MangaChaptersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MangaChaptersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MangaChapters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MangaChapters.Include(m => m.Manga);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MangaChapters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MangaChapters == null)
            {
                return NotFound();
            }

            var mangaChapter = await _context.MangaChapters
                .Include(m => m.Manga)
                .FirstOrDefaultAsync(m => m.MangaChapterId == id);
            if (mangaChapter == null)
            {
                return NotFound();
            }

            return View(mangaChapter);
        }

        // GET: MangaChapters/Create
        public IActionResult Create()
        {
            ViewData["MangaId"] = new SelectList(_context.Mangas, "MangaId", "MangaId");
            return View();
        }

        // POST: MangaChapters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MangaChapterId,MangaChapterName,MangaChapterNumber,MangaId,MangaChapterURL")] MangaChapter mangaChapter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mangaChapter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MangaId"] = new SelectList(_context.Mangas, "MangaId", "MangaId", mangaChapter.MangaId);
            return View(mangaChapter);
        }

        // GET: MangaChapters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MangaChapters == null)
            {
                return NotFound();
            }

            var mangaChapter = await _context.MangaChapters.FindAsync(id);
            if (mangaChapter == null)
            {
                return NotFound();
            }
            ViewData["MangaId"] = new SelectList(_context.Mangas, "MangaId", "MangaId", mangaChapter.MangaId);
            return View(mangaChapter);
        }

        // POST: MangaChapters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MangaChapterId,MangaChapterName,MangaChapterNumber,MangaId,MangaChapterURL")] MangaChapter mangaChapter)
        {
            if (id != mangaChapter.MangaChapterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mangaChapter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MangaChapterExists(mangaChapter.MangaChapterId))
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
            ViewData["MangaId"] = new SelectList(_context.Mangas, "MangaId", "MangaId", mangaChapter.MangaId);
            return View(mangaChapter);
        }

        // GET: MangaChapters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MangaChapters == null)
            {
                return NotFound();
            }

            var mangaChapter = await _context.MangaChapters
                .Include(m => m.Manga)
                .FirstOrDefaultAsync(m => m.MangaChapterId == id);
            if (mangaChapter == null)
            {
                return NotFound();
            }

            return View(mangaChapter);
        }

        // POST: MangaChapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MangaChapters == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MangaChapters'  is null.");
            }
            var mangaChapter = await _context.MangaChapters.FindAsync(id);
            if (mangaChapter != null)
            {
                _context.MangaChapters.Remove(mangaChapter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MangaChapterExists(int id)
        {
          return _context.MangaChapters.Any(e => e.MangaChapterId == id);
        }
    }
}
