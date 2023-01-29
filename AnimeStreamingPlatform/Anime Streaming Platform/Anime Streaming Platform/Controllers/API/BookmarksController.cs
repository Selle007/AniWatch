using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anime_Streaming_Platform.Data;
using Anime_Streaming_Platform.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Anime_Streaming_Platform.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookmarksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public BookmarksController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Bookmarks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookmark>>> GetBookmarks()
        {
            return await _context.Bookmarks.ToListAsync();
        }

        // GET: api/Bookmarks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookmark>> GetBookmark(int id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);

            if (bookmark == null)
            {
                return NotFound();
            }

            return bookmark;
        }

        // PUT: api/Bookmarks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookmark(int id, Bookmark bookmark)
        {
            if (id != bookmark.BookmarkId)
            {
                return BadRequest();
            }

            _context.Entry(bookmark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookmarkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookmarks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bookmark>> PostBookmark(Bookmark bookmark)
        {
            _context.Bookmarks.Add(bookmark);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookmark", new { id = bookmark.BookmarkId }, bookmark);
        }

        // DELETE: api/Bookmarks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmark(int id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark == null)
            {
                return NotFound();
            }

            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookmarkExists(int id)
        {
            return _context.Bookmarks.Any(e => e.BookmarkId == id);
        }




        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAnimeToBookmarks([FromBody] int animeId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (await _context.Bookmarks.AnyAsync(b => b.UserId == userId && b.AnimeId == animeId))
            {
                return BadRequest("This anime is already in your bookmarks.");
            }

            var bookmark = new Bookmark
            {
                UserId = userId,
                AnimeId = animeId
            };
            _context.Bookmarks.Add(bookmark);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<Anime>>> ListBookmarkedAnime()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var bookmarkedAnimeIds = await _context.Bookmarks
                .Where(b => b.UserId == userId)
                .Select(b => b.AnimeId)
                .ToListAsync();

            var bookmarkedAnimes = await _context.Animes
                .Where(a => bookmarkedAnimeIds.Contains(a.AnimeId))
                .ToListAsync();

            return Ok(bookmarkedAnimes);
        }



    }
}


