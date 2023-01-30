using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anime_Streaming_Platform.Data;
using Anime_Streaming_Platform.Models;

namespace Anime_Streaming_Platform.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AnimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Animes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anime>>> GetAnimes()
        {
            return await _context.Animes.ToListAsync();
        }

        // GET: api/Animes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Anime>> GetAnime(int id)
        {
            var anime = await _context.Animes.FindAsync(id);

            if (anime == null)
            {
                return NotFound();
            }

            return anime;
        }
        [Route("api/animes/{animeId}/episodes")]
        [HttpGet]
        public async Task<ActionResult<List<Episode>>> GetEpisodesByAnimeId(int animeId)
        {
            var episodes = await _context.Episodes.Where(e => e.AnimeId == animeId).ToListAsync();

            if (episodes == null)
            {
                return NotFound();
            }

            return episodes;
        }

        // DELETE: api/Animes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnime(int id)
        {
            var anime = await _context.Animes.FindAsync(id);
            if (anime == null)
            {
                return NotFound();
            }

            _context.Animes.Remove(anime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimeExists(int id)
        {
            return _context.Animes.Any(e => e.AnimeId == id);
        }


        [HttpGet("{id}/episodes")]
        public ActionResult<List<Episode>> GetEpisodes(int id)
        {
            var episodes = _context.Episodes.Where(e => e.AnimeId == id).ToList();
            if (episodes == null)
            {
                return NotFound();
            }
            return episodes;
        }


    }
}
