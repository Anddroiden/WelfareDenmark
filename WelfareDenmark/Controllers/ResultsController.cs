using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.Controllers {
    [Authorize(Policy = "IsPatient")]
    public class ResultsController : Controller {
        private readonly ApplicationDbContext _db;

        public ResultsController(ApplicationDbContext db) {
            _db = db;
        }

        public IActionResult Index() {
            var games = _db.BrainGames.Include(b => b.GameResults);
            var filteredResults = games.Select(b => new BrainGame {
                GameResults = b.GameResults.Where(r => r.Player == User.Identity.Name).ToArray(),
                Name = b.Name,
                Id = b.Id
            });
            var brainGames = filteredResults.Where(b=>b.GameResults.Any()).ToArray();
            //todo: make it so that when you click on a result on the graph, you are taken to the details page
            return View(brainGames);
        }

        public IActionResult Details(long id) {
            var gameResult = _db.Results.Include(r => r.BrainGame).First(r => r.Id == id);
            if (gameResult is null) {
                return NotFound();
            }

            return View(gameResult);
        }
    }
}