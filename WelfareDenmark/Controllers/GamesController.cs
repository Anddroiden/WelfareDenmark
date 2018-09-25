using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WelfareDenmark.Data;

namespace WelfareDenmark.Controllers {
    public class GamesController : Controller {
        private readonly ApplicationDbContext _db;

        public GamesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Reaction() {
            return View();
        }

        public IActionResult Index() {
            return View();
        }
        [HttpGet]
        public IActionResult DataEntry() {
            return View();
        }
        [HttpPost]
        public IActionResult DataEntry(GameResultDTO dto) {
            if (!ModelState.IsValid) {
                return View();
            }
            var brainGame = _db.BrainGames.First(bg=>bg.Name == dto.Name);
            _db.SaveChanges();
            return View();
        }
    }
}