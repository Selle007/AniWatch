using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anime_Streaming_Platform.Data;
using Anime_Streaming_Platform.Models;

namespace Anime_Streaming_Platform.Controllers
{

    public class EpisodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EpisodesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: EpisodesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EpisodesController/Details/5
        public async Task<IActionResult> Episode(int? id)
        {
            if (id == null || _context.Episodes == null)
            {
                return NotFound();
            }

            var episode = await _context.Episodes
                .FirstOrDefaultAsync(m => m.EpisodeId == id);
            if (episode == null)
            {
                return NotFound();
            }

            return View(episode);
        }

        // GET: EpisodesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EpisodesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EpisodesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EpisodesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EpisodesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EpisodesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
