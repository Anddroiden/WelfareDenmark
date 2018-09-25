using Microsoft.AspNetCore.Mvc;

namespace WelfareDenmark.Controllers {
    public class GamesController : Controller {
        public IActionResult Reaction() {
            return View();
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult DataEntry() {
            return View();
        }
    }
}