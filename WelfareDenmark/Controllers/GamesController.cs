using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.Controllers {
    [Authorize(Policy = "IsPatient")]
    public class GamesController : Controller {
        private readonly ApplicationDbContext _db;

        public GamesController(ApplicationDbContext db) {
            _db = db;
        }

        [HttpGet]
        public IActionResult Reaction() {
            return View();
        }
        [HttpPost]
        public IActionResult Reaction(GameResult gameResult) {
            var brainGame = _db.BrainGames.FirstOrDefault(bg => bg.Name == "Reaction");
            if (brainGame is null) {
                brainGame = new BrainGame {Name = "Reaction"};
                _db.BrainGames.Add(brainGame);
            }
            var result = new GameResult {
                BrainGame = brainGame,
                Score = gameResult.Score,
                Player = User.Identity.Name,
                DateTime = DateTime.Now
            };
            brainGame.GameResults.Add(result);
            _db.SaveChanges();
            return RedirectToAction("GameResult", new {brainGameId = brainGame.Id, gameResultId = result.Id});
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

            var brainGame = _db.BrainGames.FirstOrDefault(bg => bg.Name == dto.Name);
            if (brainGame is null) {
                brainGame = new BrainGame {Name = dto.Name};
                _db.BrainGames.Add(brainGame);
            }

            var result = new GameResult {
                Score = dto.Score,
                Player = dto.Player,
                DateTime = dto.DateTime
            };
            brainGame.GameResults.Add(result);
            _db.SaveChanges();
            return RedirectToAction("GameResult", new {brainGameId = brainGame.Id, gameResultId = result.Id});
        }

        [Route("game-result/{brainGameId}/{gameResultId}")]
        public IActionResult GameResult(long brainGameId, long gameResultId) {
            var brainGame = _db.BrainGames.Include(b => b.GameResults).FirstOrDefault(b => b.Id == brainGameId);
            var gameResult = brainGame?.GameResults?.FirstOrDefault(r => r.Id == gameResultId);
            if (gameResult is null) {
                return NotFound();
            }

            var dto = new GameResultDTO {
                Name = brainGame.Name,
                Score = gameResult.Score
            };
            return View(dto);
        }
    }
}