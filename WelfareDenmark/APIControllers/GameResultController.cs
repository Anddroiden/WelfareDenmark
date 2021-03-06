﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WelfareDenmark.Data;
using WelfareDenmark.Models;

namespace WelfareDenmark.APIControllers {
    [Route("api/Results")]
    [ApiController]
    [Authorize(Policy = PolicyConstants.IsPatient, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameResultController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public GameResultController(ApplicationDbContext context) {
            _context = context;
        }

        // api/results/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameResult([FromRoute] long id) {
            var gameResult = await _context.Results.FindAsync(id);

            if (gameResult == null) return NotFound();
            if (gameResult.Player != User.Identity.Name) return Unauthorized();

            return Ok(gameResult);
        }

        // api/results
        [HttpGet]
        public IActionResult GetAllUsersGameResults() {
            var games = _context.BrainGames.Include(b => b.GameResults);
            var filteredResults = games.Select(b => new BrainGame {
                GameResults = b.GameResults.Where(r => r.Player == User.Identity.Name).ToArray(),
                Name = b.Name,
                Id = b.Id
            });
            var brainGames = filteredResults.Where(b => b.GameResults.Any()).ToArray();
            return Ok(brainGames);
        }

        // api/results
        [HttpPost]
        public IActionResult PostGameResult([FromBody] GameResultDto gameResultDto) {
            var brainGame = _context.BrainGames.FirstOrDefault(game => game.Name == gameResultDto.Name);
            if (brainGame == null) {
                _context.Add(new BrainGame {Name = gameResultDto.Name});
//                return BadRequest();
            }

            var gameResult = new GameResult {
                Player = User.Identity.Name,
                Score = gameResultDto.Score,
                DateTime = gameResultDto.DateTime,
                BrainGame = brainGame
            };
            _context.Results.Add(gameResult);
            _context.SaveChanges();
            return Ok(gameResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameResult([FromRoute] long id) {
            var gameResult = await _context.Results.FindAsync(id);
            if (gameResult == null) return NotFound();
            if (gameResult.Player != User.Identity.Name) return Unauthorized();

            _context.Results.Remove(gameResult);
            await _context.SaveChangesAsync();
            return Ok(gameResult);
        }
    }
}