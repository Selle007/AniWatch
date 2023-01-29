using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anime_Streaming_Platform.Data;
using Anime_Streaming_Platform.Models;
using Microsoft.AspNetCore.Identity;

namespace Anime_Streaming_Platform.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookmarksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public BookmarksController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
    }

        // GET: Admin/Bookmarks
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var bookmarks = _context.Bookmarks.Where(b => b.UserId == currentUser.Id);
            return View(await bookmarks.ToListAsync());
            //var applicationDbContext = _context.Bookmarks.Include(b => b.Anime).Include(b => b.User);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Bookmarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bookmarks == null)
            {
                return NotFound();
            }

            var bookmark = await _context.Bookmarks
                .Include(b => b.Anime)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookmarkId == id);
            if (bookmark == null)
            {
                return NotFound();
            }

            return View(bookmark);
        }
        /*
        // GET: Admin/Bookmarks/Create
        public IActionResult Create()
        {
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeName");
            return View();
            
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
    
        // POST: Admin/Bookmarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookmarkId,UserId,AnimeId")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookmark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId", bookmark.AnimeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookmark.UserId);
            return View(bookmark);
        }*/
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookmarkId,AnimeId")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                bookmark.UserId = currentUser.Id;
                _context.Add(bookmark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeName", bookmark.AnimeId);
            return View(bookmark);
        }

    */

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var animes = await _context.Animes.ToListAsync();
            ViewBag.AnimeId = new SelectList(animes, "AnimeId", "AnimeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bookmark bookmark)
        {
            
               var user = await _userManager.GetUserAsync(User);
                var anime = this._context.Animes.Find(bookmark.AnimeId);
            /* bookmark.Anime = anime;
            bookmark.UserId = user.Id;


            _context.Add(bookmark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           */
            if (!await _context.Bookmarks.AnyAsync(b => b.AnimeId == bookmark.AnimeId && b.UserId == user.Id))
            {
                bookmark.UserId = user.Id;
                bookmark.Anime = anime;
                _context.Add(bookmark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Display an error message if the Anime is already in the user's Bookmarks
                ModelState.AddModelError("", "Anime is already in your Bookmarks.");
            }

            var animes = await _context.Animes.ToListAsync();
            ViewBag.AnimeId = new SelectList(animes, "AnimeId", "AnimeName");
            return View(bookmark);
        }

        // GET: Admin/Bookmarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bookmarks == null)
            {
                return NotFound();
            }

            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark == null)
            {
                return NotFound();
            }
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId", bookmark.AnimeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookmark.UserId);
            return View(bookmark);
        }

        // POST: Admin/Bookmarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookmarkId,UserId,AnimeId")] Bookmark bookmark)
        {
            if (id != bookmark.BookmarkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookmark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookmarkExists(bookmark.BookmarkId))
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
            ViewData["AnimeId"] = new SelectList(_context.Animes, "AnimeId", "AnimeId", bookmark.AnimeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookmark.UserId);
            return View(bookmark);
        }
        /*
        // GET: Admin/Bookmarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bookmarks == null)
            {
                return NotFound();
            }

            var bookmark = await _context.Bookmarks
                .Include(b => b.Anime)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookmarkId == id);
            if (bookmark == null)
            {
                return NotFound();
            }

            return View(bookmark);
        }
        
        // POST: Admin/Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bookmarks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bookmarks'  is null.");
            }
            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark != null)
            {
                _context.Bookmarks.Remove(bookmark);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark == null)
            {
                return NotFound();
            }

            return View(bookmark);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);
            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool BookmarkExists(int id)
        {
          return _context.Bookmarks.Any(e => e.BookmarkId == id);
        }


        [HttpPost]
        public async Task<IActionResult> AddBookmark(int animeId)
        {
            // retrieve the current user's ID
            var userId = User.Identity.Name;

            // check if the anime is already in the user's bookmarks
            var existingBookmark = _context.Bookmarks
                .Where(b => b.UserId == userId && b.AnimeId == animeId)
                .FirstOrDefault();

            if (existingBookmark != null)
            {
                // anime is already in bookmarks, return error message
                return BadRequest("Anime is already in bookmarks.");
            }

            // create a new bookmark
            var bookmark = new Bookmark
            {
                UserId = userId,
                AnimeId = animeId
            };

            // add the bookmark to the database
            _context.Bookmarks.Add(bookmark);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
