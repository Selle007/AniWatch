using Microsoft.AspNetCore.Mvc;

namespace Anime_Streaming_Platform.Controllers
{
    public class BookmarksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
